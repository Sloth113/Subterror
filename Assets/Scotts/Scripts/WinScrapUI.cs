﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Display total scrap value in the attached text
//Used on win screen
public class WinScrapUI : MonoBehaviour {

    void OnEnable() {
        this.GetComponent<Text>().text = GameManager.Instance.m_scrapTotal.ToString();
    }
    void OnAwake() {
        this.GetComponent<Text>().text = GameManager.Instance.m_scrapTotal.ToString();
    }
}
