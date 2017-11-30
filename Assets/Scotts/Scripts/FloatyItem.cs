using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Makes the item attached spin and float. 
public class FloatyItem : MonoBehaviour {
    public float m_rotateSpeed = 50.0f; //Degress/Sec
    public float m_bounceDist = 0.3f; //0 nothing 1 a lot 
    public float m_speed = 0.5f; // How many ups and downs 
    public bool m_xSpin = true;
    public bool m_ySpin = true;
    public bool m_zSpin = true;

    private Vector3 m_origin = new Vector3();
    
	// Use this for initialization
	void Start () {
        m_origin = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(m_xSpin? m_rotateSpeed * Time.deltaTime : 0,m_ySpin ? m_rotateSpeed * Time.deltaTime : 0, m_zSpin ? m_rotateSpeed * Time.deltaTime : 0), Space.World);
         
        // Float up/down with a Sin()
        Vector3 tempPos;
        tempPos = m_origin;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * m_speed) * m_bounceDist;

        transform.position = tempPos;
    }
}
