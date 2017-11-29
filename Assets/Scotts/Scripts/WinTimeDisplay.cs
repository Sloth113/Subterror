using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTimeDisplay : MonoBehaviour {
    void OnEnable()
    {
        this.GetComponent<Text>().text = ((int)GameManager.Instance.m_timer).ToString();
    }
    void OnAwake()
    {
        this.GetComponent<Text>().text = ((int)GameManager.Instance.m_timer).ToString();
    }
}
