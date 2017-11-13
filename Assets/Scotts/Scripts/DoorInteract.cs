using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteract : MonoBehaviour,iInteractable {
    public bool m_open = false;
    private Text m_displayText;
    public Key m_key;
    private Animator m_animator;
    // Use this for initialization
    void Start () {
        m_displayText = this.GetComponentInChildren<Text>();
        m_animator = GetComponent<Animator>();
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
        if (m_displayText.text != GetText()) {
            m_displayText.text = GetText();
        } else {
            m_displayText.text = "";
        }
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
                List<Key> keys = user.GetComponent<CharacterControllerTest>().m_keys;
                foreach(Key key in keys) {
                    if(key.info == m_key.info) {
                        Open();
                    }
                }
            }
        }
    }
}
