using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Display time taken from game manager on the attached text item (ui)
//Used in 'win' screen to display stats
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
