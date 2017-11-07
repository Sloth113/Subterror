﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptType : MonoBehaviour {

    public enum Type { OPEN, USE };
    public Type typeofprompt = Type.OPEN;
    public Text promptText;

	void Start () {
        promptText = transform.GetComponentInChildren<Text>();
        if (typeofprompt == Type.OPEN)
        promptText.text = "Open";
        if (typeofprompt == Type.USE)
        promptText.text = "Use";		
	}
}
