using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteract : MonoBehaviour,iInteractable {
    public bool m_open = false;
    //private Text m_displayText;
    private Canvas m_uIDisplay;
    public Key m_key;
    private Animator m_animator;
    // Use this for initialization
    void Start () {
        m_uIDisplay = this.GetComponentInChildren<Canvas>();
        m_animator = GetComponent<Animator>();
        Light light = GetComponentInChildren<Light>();
        light.color = m_key.glow;
        Renderer rend = GetComponentInChildren<Renderer>();
        rend.material.SetColor("_Color", m_key.glow);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void Open() {
        m_open = true;
        //Play animation
        m_animator.SetTrigger("Open");
    }

    public void DisplayToggle() {
        if(gameObject.GetComponentInChildren<Text>() != null) {
            gameObject.GetComponentInChildren<Text>().text = GetText();
        }
        m_uIDisplay.gameObject.SetActive(!m_uIDisplay.gameObject.activeSelf && !m_open);
    }

    public string GetText() {
        return "X: Open";
    }

    public void Use() {
        //Nothing
        
        if (m_key.info == "" && !m_open) {
            Open();
        }
    }

    public void Use(GameObject user) {
        
        if (m_key.info == "" && !m_open) {
            Open();
        }else {
            if(user.transform.tag == "Player") {
                List<Key> keys = GameManager.Instance.PlayerKeys();
                foreach(Key key in keys) {
                    if(key.info == m_key.info) {
                        Open();
                    }
                }
            }
        }
    }
}
