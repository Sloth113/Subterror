using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchInteract : MonoBehaviour, iInteractable {
    bool m_state = false;
    private Text m_displayText;
    public GameObject m_link;
    
    // Use this for initialization
    void Start () {
        m_displayText = this.GetComponentInChildren<Text>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisplayToggle() {
        if (m_displayText.text != GetText()) {
            m_displayText.text = GetText();
        }else {
            m_displayText.text = "";
        }
    }

    public string GetText() {
        return "Use";
    }

    public void Use() {
        if(m_link != null && m_link.GetComponent<iInteractable>() != null) {
            m_link.GetComponent<iInteractable>().Use();
        }
        m_state = !m_state;
        Debug.Log("Switched" + m_state);
    }

    public void Use(GameObject user) {
        Use();
    }
}
