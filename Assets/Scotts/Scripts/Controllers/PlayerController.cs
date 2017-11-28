using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Inventory {
    public int scrap;
    public int mutagen;
    public List<Key> keys;
}

public class PlayerController : MonoBehaviour, iHitable {
    private CharacterController m_controller;
    private Animator m_animator;

    //Stats
    public float m_speed = 10;
    private float m_maxTotalSpeed = 10;
    public float m_hp = 100;
    public float m_maxHp = 100;
    public float m_maxTotalHp = 200;
    public float m_incomeDamMod = 1.0f;
    
    //Melee
    public float m_meleeCooldown = 0.5f;
    //private float m_meleeMidHitTime = 0.25f;
    private float m_meleeTimer = 0;
    public float m_meleeMod = 1.0f;
    public float m_meleeDamage = 10.0f;
    public float m_meleeRange = 1.0f;
    public float m_meleeAngle = 90; //Degrees
    public bool m_meleeKnockBack = false;
    public bool m_meleeLifeSteal = false;

    //Ranged
    public float m_rangeCooldown = 5;
    private float m_rangeTimer = 0;
    public List<GameObject> m_bulletPrefabs; //ADD FUNCTIONS
    public Transform m_bulletExitPos;
    public int m_bulletIndex = 0;

    //Block
    public float m_blockCooldown = 5;
    private float m_blockTimer = 0;
    public float m_blockDuration = 2.0f;
    public float m_blockCounter = 0.0f;
    public float m_blockChange = 1.0f;//No damage - change 

    //Get controller and animator
    void Start() {
        m_animator = GetComponent<Animator>();
        m_controller = GetComponent<CharacterController>();
    }

    void Awake() {
        //Set new player as instance
        GameManager.Instance.NewPlayer(this.gameObject); //manager then sets on everything else

    }

    // Update is called once per frame
    void Update() {
        InControl.InputDevice input = InControl.InputManager.ActiveDevice;
        
        //Input
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * m_speed;
        if (move.sqrMagnitude >= m_speed * m_speed) {
            move = move.normalized * m_speed;
        }
        transform.LookAt(GetMouseToPlayerPlanePoint()); //look at mouse
        //Joystick
        if (input.GetControl(InControl.InputControlType.RightStickX) > 0 || input.GetControl(InControl.InputControlType.RightStickY) > 0) {
            float heading = Mathf.Atan2(input.GetControl(InControl.InputControlType.RightStickX), input.GetControl(InControl.InputControlType.RightStickY)) *Mathf.Rad2Deg;
            transform.rotation=Quaternion.Euler(0f,0f,heading);
        }


        //Moving back or forward for animation blending EXPAND to side ways as well
        if (Vector3.Dot(transform.forward, move) < 0) {
            m_animator.SetFloat("Speed", -move.magnitude / m_speed);//Backwards
        } else {
            m_animator.SetFloat("Speed", move.magnitude / m_speed);
        }
        //Keep grounded
        if (!m_controller.isGrounded) {
            //fall
            move.y += Physics.gravity.y * 10 * Time.deltaTime;
        } else {
            move.y = 0;
        }

        //Move using controller
        m_controller.Move(move * Time.deltaTime);

        //Range shoot input.
        if (m_animator.GetCurrentAnimatorStateInfo(1).IsName("UpperBody.Movement")) {
            // Debug.Log("Moving");
            if (Input.GetButton("Fire1") && m_rangeTimer >= m_rangeCooldown && m_bulletPrefabs.Count > 0 && m_bulletExitPos != null) {
                m_animator.SetTrigger("Shoot");
                //CreateBullet();
                m_rangeTimer = 0;
            }//Melee
            else if (Input.GetButton("Fire2") && m_meleeTimer >= m_meleeCooldown) {
                m_animator.SetTrigger("Melee");
                //Hit check PUT IN FUNCTION FOR ANIMATOR TO CALL
              //  MeleeSwing();
                //Set timer to 0 
                m_meleeTimer = 0;
            } else if (Input.GetButton("Fire3") && m_blockTimer >= m_blockCooldown) {
                //Blocking
                m_animator.SetTrigger("Block");
                m_incomeDamMod -= m_blockChange; //drop the mod 
                m_blockCounter = 0.01f; //Counts up to duration MATCH WITH ANIMATION?
                m_blockTimer = 0; //Cooldown 
            }
        }
        //Heal

        //Switch weapon

        //
        
        //Timers
        if (m_rangeTimer < m_rangeCooldown) {
            m_rangeTimer += Time.deltaTime;
        }
        if (m_meleeTimer < m_meleeCooldown) {
            m_meleeTimer += Time.deltaTime;
        }
        if (m_blockTimer < m_blockCooldown) {
            m_blockTimer += Time.deltaTime;
        }
        //If not 0 increase until hits duration
        if (m_blockCounter > 0 && m_blockCounter < m_blockDuration) {
            m_blockCounter += Time.deltaTime;
        }
        //If its out of duration set block damage back to normal
        if (m_blockCounter >= m_blockDuration) {
            m_incomeDamMod += m_blockChange;//reset modifier
            m_blockCounter = 0.0f;
        }

    }

    public void MeleeSwing() {
        RaycastHit hit;
        //SphereCast(origin[position<foot> + controller displacement], 
        if (Physics.SphereCast(transform.position + m_controller.center - transform.forward, m_controller.height / 1.5f, transform.forward, out hit, 0.75f)) {
            Debug.Log(hit.transform.name); //Works        
            if (hit.transform.gameObject.GetComponent<iHitable>() != null) {
                hit.transform.gameObject.GetComponent<iHitable>().Hit((int)(m_meleeDamage * m_meleeMod));
                if (m_meleeKnockBack) {
                    hit.transform.gameObject.GetComponent<iHitable>().Knockback();
                }
                if (m_meleeLifeSteal) {
                    IncreaseCurrentHP((int)(m_meleeDamage / 2.0f));//Heal partial
                }
            }
        }
    }

    public void CreateBullet() {
        Instantiate<GameObject>(m_bulletPrefabs[m_bulletIndex], m_bulletExitPos.transform.position, m_bulletExitPos.transform.rotation);//make transform postition the point on the gun
    }


    //Used to use mouse on board
    private Vector3 GetMouseToPlayerPlanePoint() {

        Vector3 mousePos = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

        Plane playersYPlane = new Plane(Vector3.up, transform.position);
        float rayDist = 0;

        playersYPlane.Raycast(mouseRay, out rayDist);

        Vector3 castPoint = mouseRay.GetPoint(rayDist);
        return castPoint;
    }

    void OnTriggerEnter(Collider c) {
        //Items
        if (c.transform.tag == "Item") {
            string itemName = c.GetComponent<iPickUp>().GetItem();
            if (itemName.Contains("Key")) {
                Key k = c.GetComponent<KeyScript>().GetKey();
                GameManager.Instance.AddKey(k);
                //m_keys.Add(k);
            } else if (itemName == "Scrap") {
                GameManager.Instance.ChangeScrap(1);
            } else if (itemName == "MutaGen") {
                GameManager.Instance.ChangeMutaGen(1);
            }
            c.GetComponent<PlaySoundOnDestroy>().CreateTempSoundObj();
            Destroy(c.gameObject);
        }
        if (c.transform.tag == "Interactable") {
            if (c.GetComponent<LadderInteract>() != null) {
                c.GetComponent<iInteractable>().Use(this.gameObject);
            }
            c.GetComponent<iInteractable>().DisplayToggle();
        }
    }
    //leaving interactable space
    void OnTriggerExit(Collider c) {
        if (c.transform.tag == "Interactable") {
            c.GetComponent<iInteractable>().DisplayToggle();
        }
    }
    void OnTriggerStay(Collider c) {
        if (c.transform.tag == "Interactable" && Input.GetKeyDown(KeyCode.X)) {
            c.GetComponent<iInteractable>().Use(this.gameObject);
        }
    }

    private void OnDeath() {
        //GameManager.m_instance.
        m_animator.SetTrigger("Dead");
        this.enabled = false;
        GameManager.Instance.InGameToDead();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Destart
    }

    public void Hit() {
        m_hp--;
        if (m_hp <= 0) {
            OnDeath();
        }else {
            m_animator.SetTrigger("Hit");
        }
    }
    public void Hit(int dam) {
        m_hp -= dam;
        if (m_hp <= 0) {
            OnDeath();
        }else {
            m_animator.SetTrigger("Hit");
        }
        
    }
    //Heal
    public void IncreaseCurrentHP(int amount) {
        m_hp += amount;

        if(m_hp > m_maxHp) {
            m_hp = m_maxHp;
        }
    }
    public void IncreaseTotalHP(int amount) {
        m_maxHp += amount;
        if (m_maxHp > m_maxTotalHp) {
            m_maxHp = m_maxTotalHp;
        }
        IncreaseCurrentHP(amount);
    }

    public void Knockback() {
        Debug.Log("Stagger");
    }

    public void Knockback(Vector3 dir) {
        Debug.Log("Push back");
    }
}
