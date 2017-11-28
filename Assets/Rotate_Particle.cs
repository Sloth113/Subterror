using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Particle : MonoBehaviour {
	public float rotateSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.back * Time.deltaTime * rotateSpeed);
	}
}
