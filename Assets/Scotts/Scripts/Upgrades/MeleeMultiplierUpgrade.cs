using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeMultiplierUpgrade : MonoBehaviour , iUpgrade, ISelectHandler{
    public float m_damMulti = 1.0f;
    public upgradeDetails m_info;
    public string m_details = "Change the players melee to hit harder";

    public void Apply(GameObject player) {
        CharacterControllerTest playerScript = player.GetComponent<CharacterControllerTest>();
        if (playerScript != null) {
            playerScript.meleeMod += m_damMulti;
        }
    }
    public upgradeDetails CostAmount() {
        return m_info;
    }

    public string GetDetails() {
        return m_details + m_info.cost + " scrap";
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
       //Info
    }
}
