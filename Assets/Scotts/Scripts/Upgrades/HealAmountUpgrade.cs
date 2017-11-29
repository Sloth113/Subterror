using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAmountUpgrade : iUpgrade {
    public int m_healChange = 5;
    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            playerScript.m_healAmount += m_healChange;
        }
    }

    public override bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades) {
        if (inv.scrap >= m_info.cost) {
            return true;
        }
        return false;
    }
    public new void AddToPlayer() {
        base.AddToPlayer();
    }

}
