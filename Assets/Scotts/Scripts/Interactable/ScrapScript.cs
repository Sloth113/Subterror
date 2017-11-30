using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Scrap pick up used to grab details
public class ScrapScript : MonoBehaviour, iPickUp {
    public string GetItem() {
        return "Scrap";
    }
}
