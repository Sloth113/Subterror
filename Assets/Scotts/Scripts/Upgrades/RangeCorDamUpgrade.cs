using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangeCorDamUpgrade : iUpgrade {
    public int m_damageChange = 5;

    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            foreach(GameObject bullet in playerScript.m_bulletPrefabs) {
                if (bullet.GetComponent<CorrosiveBullet>() != null) {
                    bullet.GetComponent<CorrosiveBullet>().m_corrsiveEffect.GetComponent<CorrosiveEffect>().m_damage += m_damageChange;
                }
            }
            
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
