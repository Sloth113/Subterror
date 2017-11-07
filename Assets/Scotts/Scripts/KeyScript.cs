using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour, iPickUp {
    public string m_keyInfo;
    public string GetItem() {
        return "Key " + m_keyInfo;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
