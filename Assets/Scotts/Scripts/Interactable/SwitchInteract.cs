using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchInteract : MonoBehaviour, iInteractable {
    bool m_state = false;
    //private Text m_displayText;
    private Canvas m_uIDisplay;
    public GameObject m_link;
    private Animator m_animator;


    // Use this for initialization
    void Start () {
        
        m_uIDisplay = this.GetComponentInChildren<Canvas>();
        m_uIDisplay.gameObject.SetActive(false);
        //m_displayText = this.GetComponentInChildren<Text>();
        m_animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void DisplayToggle() {
        m_uIDisplay.gameObject.SetActive(!m_uIDisplay.gameObject.activeSelf && !m_state);
    }

    public string GetText() {
        return "X: Use";
    }

    public void Use()  {
        m_animator.SetTrigger("Used");
        if (m_link != null && m_link.GetComponent<iInteractable>() != null) {
            m_link.GetComponent<iInteractable>().Use();
        }
        m_state = true;
        Debug.Log("Switched" + m_state);
    }

    public void Use(GameObject user) {
        Use();
    }
}
