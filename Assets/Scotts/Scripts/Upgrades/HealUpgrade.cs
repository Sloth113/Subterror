using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealUpgrade : iUpgrade {
    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            playerScript.m_healCost = 10;
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
