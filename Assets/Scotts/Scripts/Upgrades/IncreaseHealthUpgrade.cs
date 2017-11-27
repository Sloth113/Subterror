using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IncreaseHealthUpgrade : iUpgrade {
    public int m_increaseAmount = 10;

    public override void Apply(GameObject player) {
        CharacterControllerTest playerScript = player.GetComponent<CharacterControllerTest>();
        if(playerScript != null) {
            playerScript.m_hp += m_increaseAmount;
            playerScript.maxHp += m_increaseAmount;
        }
    }

    public override bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades) {
        if(inv.mutagen > m_info.cost) {
            return true;
        }
        return false;
    }
}
