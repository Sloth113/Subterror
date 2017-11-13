using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Key {
    public string info;
    public Color glow;
}
public class KeyScript : MonoBehaviour, iPickUp {
    public Key m_key;
    public string GetItem() {
        return "Key " + m_key.info;
    }

    public Key GetKey() {
        return m_key;
    }

    // Use this for initialization
    void Start () {
        Light light = GetComponentInChildren<Light>();
        light.color = m_key.glow;
        Renderer rend = GetComponentInChildren<Renderer>();
        rend.material.SetColor("_Color", m_key.glow);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
