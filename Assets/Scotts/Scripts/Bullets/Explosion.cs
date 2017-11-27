using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    private ParticleSystem m_particles;
    public float m_explosionSize = 3; //Goes with speed and emissions
    public float m_explosionDamage = 10;
	// Use this for initialization
	void Start () {
        m_particles = GetComponent<ParticleSystem>();
       // m_particles.main.startSpeed = m_explosionSize;
        m_particles.startSpeed = m_explosionSize;//doesnt work 
        //EXPLOSION
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_explosionSize);
        Debug.Log(hitColliders.Length);
        foreach (Collider hit in hitColliders) {
            Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.GetComponent<iHitable>() != null) {
                hit.transform.gameObject.GetComponent<iHitable>().Hit((int)(m_explosionDamage));
                hit.transform.gameObject.GetComponent<iHitable>().KnockBack();
            }
        }
    }
}
