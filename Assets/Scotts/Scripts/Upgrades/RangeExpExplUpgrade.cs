using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RangeExpExplUpgrade : iUpgrade {
    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            foreach (GameObject bullet in playerScript.m_bulletPrefabs) {
                if (bullet.GetComponent<ExplosiveBullet>() != null) {
                    bullet.GetComponent<ExplosiveBullet>().m_betterExplosion = true; 
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
}
