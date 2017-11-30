using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Sets text to current mutagen amount 
//Used in mutagen upgrade scene
public class ScrapUILoad : MonoBehaviour {

    void OnEnable() {
        this.GetComponent<Text>().text = GameManager.Instance.MutaGenAmount().ToString();
    }
    void OnAwake() {
        this.GetComponent<Text>().text = GameManager.Instance.MutaGenAmount().ToString();
    }

}
