using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControllerTest : MonoBehaviour, iHitable {

    private CharacterController controller;
    private Animator m_animator;

    //Stats
    public float speed = 10;
    public float m_hp = 100;
    public float maxHp = 100;
    public float incomeDamMod = 1.0f;
    private float m_MAXSPEED = 5;
    
    //Melee
    public float meleeCooldown = 0.5f;
    private float meleeTimer = 0;
    public float meleeMod = 1.0f;
    public float meleeDamage = 10.0f;
    public float meleeRange = 1.0f;
    public float meleeAngle = 90; //Degrees

    //Ranged
    public float shootCooldown = 5;
    private float shootTimer  =0;
    public GameObject bulletTest;
    public Transform bulletExit;

    //Block
    public float blockCooldown = 5;
    private float blockTimer = 0;
    public float blockDuration = 2.0f;
    public float blockCounter = 0.0f;
    public float blockChange = 1.0f;//No damage - change 


    //Inventory
    public List<Key> m_keys;
    public int m_scrap =0;
    public int m_mutagen =0;
	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        m_keys = new List<Key>();
        m_MAXSPEED = speed;
	}

    void Awake() {
        //Set new player as instance
        GameManager.Instance.NewPlayer(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        //Input
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
        if(move.sqrMagnitude >= m_MAXSPEED*m_MAXSPEED) {
            move = move.normalized * m_MAXSPEED;
        }
        
        transform.LookAt(getMouseToPlayerPlanePoint()); //look at mouse

        //Debug.Log("Dot thing" + Vector3.Dot(transform.forward, move));
        if (Vector3.Dot(transform.forward, move) < 0) {
            m_animator.SetFloat("Speed", -move.magnitude / m_MAXSPEED);//Backwards
        } else {
            m_animator.SetFloat("Speed", move.magnitude / m_MAXSPEED);
        }
        

        //Keep grounded
        if (!controller.isGrounded) {
            //fall
            move.y += Physics.gravity.y * 10 *  Time.deltaTime;
        } else {
            move.y = 0;
        }

        //Move
        controller.Move(move * Time.deltaTime);

        //Shoot
        
        if(Input.GetButton("Fire1") && shootTimer >= shootCooldown && bulletTest != null && bulletExit != null) {
            m_animator.SetTrigger("Shoot");
            Instantiate<GameObject>(bulletTest, bulletExit.transform.position, bulletExit.transform.rotation);//make transform postition the point on the gun
            shootTimer = 0;
        }//Melee
        else if(Input.GetButton("Fire2") && meleeTimer >= meleeCooldown) {
            //Sphere case in front?
            Debug.Log("Melee");
            m_animator.SetTrigger("Melee");
            //
            RaycastHit hit;
            //SphereCast(origin[position<foot> + controller displacement], 
            if (Physics.SphereCast(transform.position + controller.center - transform.forward, controller.height/1.5f, transform.forward, out hit, 0.75f)) {
                Debug.Log(hit.transform.name); //Works        
                if (hit.transform.gameObject.GetComponent<iHitable>() != null) {
                    hit.transform.gameObject.GetComponent<iHitable>().Hit((int)(meleeDamage * meleeMod));
                }
            }
            //
            meleeTimer = 0;
        } else if(Input.GetButton("Fire3") && blockTimer >= blockCooldown) {
            m_animator.SetTrigger("Block");
            incomeDamMod -= blockChange;
            blockCounter = 0.01f;
            blockTimer = 0;
        }



        //Timers
        if (shootTimer < shootCooldown) {
            shootTimer += Time.deltaTime;
        }
        if (meleeTimer < meleeCooldown) {
            meleeTimer += Time.deltaTime;
        }
        if(blockTimer < blockCooldown) {
            blockTimer += Time.deltaTime;
        }
        if(blockCounter > 0 && blockCounter < blockDuration) {
            blockCounter += Time.deltaTime;
        }
        if(blockCounter >= blockDuration) {
            incomeDamMod += blockChange;//reset modifier
            blockCounter = 0.0f;
        }

    }


    //Used to use mouse on board
    private Vector3 getMouseToPlayerPlanePoint() {

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
        if(c.transform.tag == "Item") {
            string itemName = c.GetComponent<iPickUp>().GetItem();
            if (itemName.Contains("Key")){
                Key k = c.GetComponent<KeyScript>().GetKey();
                m_keys.Add(k);
            }else if(itemName == "Scrap") {
                m_scrap++;
            }else if(itemName == "MutaGen") {
                m_mutagen++;
            }
            Destroy(c.gameObject);
        }
        if(c.transform.tag == "Interactable") {
            if(c.GetComponent<LadderInteract>() != null) {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Destart
    }

    public void Hit() {
        m_hp--;
        if (m_hp <= 0) {
            OnDeath();
        }
    }
    public void Hit(int dam) {
        m_hp -= dam;
        if (m_hp <= 0) {
            OnDeath();
        }
    }
}
