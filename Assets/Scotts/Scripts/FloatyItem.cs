using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyItem : MonoBehaviour {
    public float m_rotateSpeed = 50.0f; //Degress/Sec
    public float m_bounceDist = 0.3f; //0 nothing 1 a lot 
    public float m_speed = 0.5f; // How many ups and downs 

    private Vector3 m_origin = new Vector3();
    
	// Use this for initialization
	void Start () {
        m_origin = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(m_rotateSpeed * Time.deltaTime, m_rotateSpeed * Time.deltaTime, 0.0f), Space.World);

        // Float up/down with a Sin()
        Vector3 tempPos;
        tempPos = m_origin;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * m_speed) * m_bounceDist;

        transform.position = tempPos;
    }
}
