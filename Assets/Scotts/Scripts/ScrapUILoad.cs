using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapUILoad : MonoBehaviour {

    void OnEnable() {
        this.GetComponent<Text>().text = GameManager.Instance.MutaGenAmount().ToString();
    }
    void OnAwake() {
        this.GetComponent<Text>().text = GameManager.Instance.MutaGenAmount().ToString();
    }

}
