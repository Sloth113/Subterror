using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMutaUI : MonoBehaviour {
    void OnEnable() {
        this.GetComponent<Text>().text = GameManager.Instance.m_mutaGenTotal.ToString();
    }
    void OnAwake() {
        this.GetComponent<Text>().text = GameManager.Instance.m_mutaGenTotal.ToString();
    }
}
