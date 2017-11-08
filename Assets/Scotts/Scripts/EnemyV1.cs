using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyV1 : MonoBehaviour, iHitable {
    public int m_hp = 30;
    public float moveSpeed = 3.0f;
    //
    public GameObject target;
    public float distToTarget = 0;
    public bool melee = true;
    public bool ranged = false;
    //
    public float meleeDamage = 1.0f;
    public float meleeCooldown = 1.0f;//Seconds
    public float rangedCooldown = 5.0f;
    private float meleeTimer = 0.0f;
    private float rangedTimer = 0.0f;
    //
    public GameObject m_rangeProjectile;
    public Transform m_projectileExit;
    //
    private CharacterController controller;
    private NavMeshAgent navAgent;
    private ItemDropSystem m_dropSystem;

    // Use this for initialization
    void Start () {
        controller = this.GetComponent<CharacterController>();
        navAgent = this.GetComponent<NavMeshAgent>();
        m_dropSystem = GetComponent<ItemDropSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        //Move parts
		if(target != null) {
            //Check dis
            if((target.transform.position - this.transform.position).magnitude > distToTarget) {
                Vector3 dir = target.transform.position - this.transform.position;
                dir.Normalize();

                navAgent.destination = target.transform.position - (dir * distToTarget);
            }else if (ranged) {
                Vector3 lookAtPos = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
                
                transform.LookAt(lookAtPos);
            }
            //Move towards
            /*if((target.transform.position - this.transform.position).magnitude > distToTarget) {
                Vector3 dir = target.transform.position - this.transform.position;
                dir.Normalize();
                if (!controller.isGrounded) {
                    //fall
                    dir.y += Physics.gravity.y * 10 * Time.fixedDeltaTime;
                } else {
                    dir.y = 0;
                }
                controller.Move(dir * moveSpeed * Time.deltaTime);
            } 
            */
        }

        //Attack parts
        //Melee
        if (melee && meleeTimer > meleeCooldown && (target.transform.position - this.transform.position).magnitude <2) {
            //Melee attack
            Debug.Log("Enemy Melee Attack");
            RaycastHit hit;

            if (Physics.SphereCast(transform.position + controller.center - transform.forward, controller.height / 1.5f, transform.forward, out hit, 0.75f)) {
                Debug.Log(hit.transform.name); //Works        
                if (hit.transform.gameObject.GetComponent<iHitable>() != null) {
                    hit.transform.gameObject.GetComponent<iHitable>().Hit((int)(meleeDamage));
                }
            }
            meleeTimer = 0.0f;
        }else {
            meleeTimer += Time.deltaTime;
        }
        //Ranged
        
        if (ranged && rangedTimer > rangedCooldown && (target.transform.position - this.transform.position).magnitude > 2.0f) {
            //Range attack
            //Make sure look at player 
            Debug.Log("Enemy Range Attack");
            Instantiate<GameObject>(m_rangeProjectile, m_projectileExit.transform.position, m_projectileExit.transform.rotation);//make transform postition the point on the gun
            rangedTimer = 0.0f;
        } else {
            rangedTimer += Time.deltaTime;
        }
        
    }
    private void OnDeath() {
        //Do stuff animation n shit then die? 
        if (m_dropSystem != null) {
            m_dropSystem.DropItems();
        }
        Destroy(this.gameObject);
    }

    public void Hit() {
        m_hp--;
        if(m_hp <= 0) {
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
