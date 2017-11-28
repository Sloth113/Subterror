using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyV2 : MonoBehaviour, iHitable {
    public int m_hp = 30;
    private int m_maxHp;
    public float m_moveSpeed = 3.0f;
    //
    public GameObject m_target;
    public float m_distToTarget = 0;
    public float m_agroRange = 20;
    public bool m_melee = true;
    public bool m_ranged = false;
    public bool m_explody = false;
    //
    public float m_meleeDamage = 1.0f;
    public float m_meleeCooldown = 1.0f;//Seconds
    public float m_meleeRange = 2.0f;
    public float m_rangedCooldown = 5.0f;
    private float m_meleeTimer = 0.0f;
    private float m_rangedTimer = 0.0f;
    //
    public GameObject m_rangeProjectile;
    public Transform m_projectileExit;
    [Header("Overhead Healther Bar")]
    public GameObject m_hPCanvas;
    public Image m_healthBar;
    //
    private CharacterController m_controller;
    private NavMeshAgent m_navAgent;
    private ItemDropSystem m_dropSystem;
    private Animator m_animator;


    // Use this for initialization
    void Start () {
        m_maxHp = m_hp;
        m_controller = this.GetComponent<CharacterController>();
        m_navAgent = this.GetComponent<NavMeshAgent>();
        m_dropSystem = GetComponent<ItemDropSystem>();
        m_animator = GetComponent<Animator>();
        if(m_target == null) {
            m_target  = GameObject.FindGameObjectWithTag("Player");
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Move parts
		if(m_target != null && (m_target.transform.position - this.transform.position).magnitude < m_agroRange) {
            //Get to attack range  (MOVE)
            if((m_target.transform.position - this.transform.position).magnitude > m_distToTarget) {
                Vector3 dir = m_target.transform.position - this.transform.position;
                dir.Normalize();//direction to target
                m_navAgent.destination = m_target.transform.position - (dir * m_distToTarget);//Move to closest point within range towards target 
            }else if (m_ranged) {
                //Have the enemy always aiming at target
                Vector3 lookAtPos = new Vector3(m_target.transform.position.x, this.transform.position.y, m_target.transform.position.z); //no y?
                transform.LookAt(lookAtPos);
            }
            
            m_animator.SetFloat("Speed", m_navAgent.velocity.magnitude / m_navAgent.speed); //Blend tree 
        }

        //Attack parts
        //Melee
        if (m_melee && m_meleeTimer > m_meleeCooldown && (m_target.transform.position - this.transform.position).magnitude < m_meleeRange) {
            //Melee attack
            //Debug.Log("Enemy Melee Attack");
            if (m_explody) {
                //Close enough to explode
                OnDeath();
            }
            m_animator.SetTrigger("Melee");
            MeleeSwing(); //MAY REMOVE FOR ANIMATION TO CONTROL 
            m_meleeTimer = 0.0f;
        }else {
            m_meleeTimer += Time.deltaTime;
        }
        //Ranged
        
        if (m_ranged && m_rangedTimer > m_rangedCooldown && (m_target.transform.position - this.transform.position).magnitude > 2.0f) {
            //Range attack
            //Make sure look at player 
            //Debug.Log("Enemy Range Attack");
            m_animator.SetTrigger("Shoot");
            CreateBullet();//MAY BE REMOVED FOR ANIMATOR CALLS
            m_rangedTimer = 0.0f;
        } else {
            m_rangedTimer += Time.deltaTime;
        }
        
    }
    public void MeleeSwing() {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position + m_controller.center - transform.forward, m_controller.height / 1.5f, transform.forward, out hit, 1f)) {
            Debug.Log(hit.transform.name); //Works        
            if (hit.transform.gameObject.GetComponent<iHitable>() != null) {
                hit.transform.gameObject.GetComponent<iHitable>().Hit((int)(m_meleeDamage));
            }
        }
    }
    public void CreateBullet() {
        Instantiate<GameObject>(m_rangeProjectile, m_projectileExit.transform.position, m_projectileExit.transform.rotation);//make transform postition the point on the gun
    }
    private void OnDeath() {
        //Do stuff animation n shit then die? 
        if (m_dropSystem != null) {
            m_dropSystem.DropItems();
        }
        if (m_explody) {
            Instantiate<GameObject>(m_rangeProjectile, m_projectileExit.transform.position, m_projectileExit.transform.rotation);//Range projectile in this case is explosion prefab
        }
        Destroy(this.gameObject);
    }

    public void Hit() {
        m_hp--;
        if (m_hPCanvas != null) {
            m_hPCanvas.SetActive(true);
            m_healthBar.fillAmount = (float)m_hp / m_maxHp;
        }
        if (m_hp <= 0) {
            OnDeath();
        }
    }
    public void Hit(int dam) {        
        m_hp -= dam;
        if (m_hPCanvas != null) {
            m_hPCanvas.SetActive(true);
            m_healthBar.fillAmount =  (float)m_hp/ m_maxHp;
        }
        if (m_hp <= 0) {
            OnDeath();
        }
    }

    public void KnockBack() {
        Debug.Log("Stagger");
    }

    public void KockBack(Vector3 dir) {
        Debug.Log("Push back");
    }
}
