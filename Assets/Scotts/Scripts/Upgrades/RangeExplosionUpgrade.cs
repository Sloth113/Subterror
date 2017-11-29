﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeExplosionUpgrade : iUpgrade {
    public GameObject m_newExplosionSize; 

    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            playerScript.m_bulletPrefabs.Add(m_newExplosionSize);
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
