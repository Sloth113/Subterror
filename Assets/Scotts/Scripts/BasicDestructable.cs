﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDestructable : MonoBehaviour, iHitable {
    public int m_hp = 20;
    public List<GameObject> m_particleObjects;
    private ItemDropSystem m_dropSystem;
	// Use this for initialization
	void Start () {
        m_dropSystem = GetComponent<ItemDropSystem>();
     }
	
	// Update is called once per frame
	void Update () {

	}

    void OnDeath() {
        //Explosion
        if (m_particleObjects != null && m_particleObjects.Count > 0) {
            foreach (GameObject particleObj in m_particleObjects) {
                GameObject effect = Instantiate<GameObject>(particleObj, this.transform.position, this.transform.rotation);
                ParticleSystem particle = effect.GetComponent<ParticleSystem>();
                particle.Play();
            }
            Destroy(this.gameObject);
        }else {
            Destroy(this.gameObject); //vanish 
        }
        //Drop system
        if(m_dropSystem != null) {
            m_dropSystem.DropItems();
        }        
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