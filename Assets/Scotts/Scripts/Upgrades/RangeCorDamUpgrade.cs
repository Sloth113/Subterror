using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangeCorDamUpgrade : iUpgrade {
    public float m_rangeMod = 1.0f;

    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            //playerScript.m_bulletPrefabs[1].GetComponent<CorrosiveBullet>().IncreaseDamage();
        }
    }

    public override bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades) {
        if (inv.mutagen > m_info.cost) {
            return true;
        }
        return false;
    }
}
