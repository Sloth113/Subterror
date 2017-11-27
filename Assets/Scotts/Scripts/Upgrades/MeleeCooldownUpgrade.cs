using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeCooldownUpgrade : MonoBehaviour, iUpgrade, ISelectHandler {
    public int m_cooldownChange = 10;
    public upgradeDetails m_info;
    public string m_details = "Change the players melee to be able to hit more";

    public void Apply(GameObject player) {
        CharacterControllerTest playerScript = player.GetComponent<CharacterControllerTest>();
        if (playerScript != null) {
            //Toggle on player
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
        //Change text in UI to give details and costs n stuff
    }
}
