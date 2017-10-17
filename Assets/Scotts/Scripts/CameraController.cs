using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private Vector3 m_dist;
    public float smoothTime = 3.0f;
    private Vector3 velocty = Vector3.zero;
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

            //this.transform.position = player.transform.position - m_dist;
            Vector3 targetLoc = (player.transform.position - m_dist);
            // this.transform.position = Vector3.Lerp(this.transform.position, targetLoc, smoothTime);
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetLoc, ref velocty,smoothTime);
        }
	}
}
