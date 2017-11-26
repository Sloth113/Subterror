using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttackUpgrade : MonoBehaviour, iUpgrade {
    public int m_increaseAmount = 10;
    public upgradeDetails m_info;

    public void Apply(GameObject player) {
        CharacterControllerTest playerScript = player.GetComponent<CharacterControllerTest>();
        if (playerScript != null) {
            playerScript.meleeDamage += (float)m_increaseAmount / 10.0f;
         //   playerScript.m_hp += m_increaseAmount;
         //   playerScript.maxHp += m_increaseAmount;
        }

    }

    public upgradeDetails CostAmount() {
        return m_info;
    }

    public string GetDetails() {
        return "Increase players melee Attack by " + m_increaseAmount/10.0f + " for " + m_info.cost + " MutaGen";
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
}
