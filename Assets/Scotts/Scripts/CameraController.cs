using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private Vector3 m_dist;
	// Use this for initialization
	void Start () {
        if(player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (player != null) {
            m_dist = player.transform.position - this.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null) {
            this.transform.position = player.transform.position - m_dist;
        }
	}
}
