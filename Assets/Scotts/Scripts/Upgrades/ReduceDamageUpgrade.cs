using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceDamageUpgrade : iUpgrade {
    public float m_reduceAmount = 0.25f; 

    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            playerScript.m_blockChange += m_reduceAmount;
        }
    }

    public override bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades) {
        if (inv.mutagen >= m_info.cost) {
            return true;
        }
        return false;
    }

    public new void AddToPlayer() {
        base.AddToPlayer();
    }
}
