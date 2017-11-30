using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Sets text to current scrap amount 
//Used in scrap upgrade scene
public class MutagenUILoad : MonoBehaviour {
    void OnEnable() {
    this.GetComponent<Text>().text = GameManager.Instance.MutaGenAmount().ToString();
    }
    void OnAwake() {
    this.GetComponent<Text>().text = GameManager.Instance.MutaGenAmount().ToString();
    }
}
