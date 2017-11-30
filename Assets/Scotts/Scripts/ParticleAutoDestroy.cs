using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Particles are made as prefabs, to clean up the scene this script deletes them once they stop playing.
public class ParticleAutoDestroy : MonoBehaviour {
    private ParticleSystem particles;
	// Use this for initialization
	void Start () {
        particles = GetComponent<ParticleSystem>();
	}
	
	// checks to see if the particle system is functioning, if not destroys the prefab 
	void Update () {
		if(particles != null && !particles.IsAlive()) {
            Destroy(this.gameObject);
        }
            
	}
}
