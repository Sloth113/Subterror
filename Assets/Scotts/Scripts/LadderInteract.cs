using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteract : MonoBehaviour, iInteractable {
    public string m_sceneName = "";
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DisplayToggle() {
        //Nothing
    }

    public string GetText() {
        return "To level" + m_sceneName;
    }

    public void Use() {
        GameManager.Instance.NextLevel(m_sceneName);
    }

    public void Use(GameObject user) {
        Use();
    }
}
