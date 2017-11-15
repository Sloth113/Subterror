using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealthUpgrade : MonoBehaviour, iUpgrade {
    public int increaseAmount = 10;
    public void Apply(GameObject player) {
        CharacterControllerTest playerScript = player.GetComponent<CharacterControllerTest>();
        if(playerScript != null) {
            playerScript.m_hp += increaseAmount;
            playerScript.maxHp += increaseAmount;
        }
        
    }
}
