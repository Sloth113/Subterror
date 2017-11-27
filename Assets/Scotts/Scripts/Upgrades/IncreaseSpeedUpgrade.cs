﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IncreaseSpeedUpgrade : MonoBehaviour, iUpgrade, ISelectHandler {
    public int m_increaseAmount = 10;
    public upgradeDetails m_info;
    public string m_details; 

    public void Apply(GameObject player) {
        CharacterControllerTest playerScript = player.GetComponent<CharacterControllerTest>();
        if (playerScript != null) {
            playerScript.speed += m_increaseAmount;
        }

    }

    public upgradeDetails CostAmount() {
        return m_info;
    }

    public string GetDetails() {
        return "Increase players speed by " + m_increaseAmount + " for " + m_info.cost + " MutaGen";
    }
    public void DebugInfo() {
        Debug.Log(GetDetails());
    }

    public bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades) {
        if (inv.mutagen > m_info.cost) {
            return true;
        }
        return false;
    }

    public void OnSelect(BaseEventData eventData) {
        //Change text in UI to give details and costs n stuff
    }
}