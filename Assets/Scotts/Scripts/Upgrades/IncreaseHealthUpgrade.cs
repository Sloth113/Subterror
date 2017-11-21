using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealthUpgrade : MonoBehaviour, iUpgrade {
    public int m_increaseAmount = 10;
    public upgradeDetails m_info;
    
    public void Apply(GameObject player) {
        CharacterControllerTest playerScript = player.GetComponent<CharacterControllerTest>();
        if(playerScript != null) {
            playerScript.m_hp += m_increaseAmount;
            playerScript.maxHp += m_increaseAmount;
        }
        
    }

    public upgradeDetails CostAmount() {
        return m_info;
    }

    public string GetDetails() {
        return "Increase players health by " + m_increaseAmount + " for " + m_info.cost + " MutaGen";
    }
    public void DebugInfo() {
        Debug.Log(GetDetails());
    }
}
