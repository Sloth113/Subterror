using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterLifestealUpgrade : iUpgrade {
    public float m_newRatio = 0.5f;
    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            playerScript.m_lifeStealRatio = m_newRatio;
        }
    }

    public override bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades) {
        if (inv.scrap > m_info.cost) {
            return true;
        }
        return false;
    }
    public new void AddToPlayer() {
        base.AddToPlayer();
    }
}
