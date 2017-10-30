using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelScript : MonoBehaviour {
    public int hp = 2;
    public GameObject explosion;
    private ItemDropSystem m_dropSystem;
	// Use this for initialization
	void Start () {
        m_dropSystem = GetComponent<ItemDropSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1")) {
            OnDeath();
        }
	}

    void OnDeath() {
        //Explosion
        if (explosion != null) {
            GameObject exploison = Instantiate<GameObject>(explosion, this.transform.position, this.transform.rotation);
            ParticleSystem particle = explosion.GetComponent<ParticleSystem>();
            particle.Play();
            Destroy(this.gameObject);
        }else {
            Destroy(this.gameObject);
        }
        if(m_dropSystem != null) {
            m_dropSystem.DropItems();
        }        
    }
}
