using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RangeExpRadiUpgrade : iUpgrade {
    public float m_radiIncrease = 1.0f; //

    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            foreach (GameObject bullet in playerScript.m_bulletPrefabs) {
                if (bullet.GetComponent<ExplosiveBullet>() != null) {
                    bullet.GetComponent<ExplosiveBullet>().m_explosion.GetComponent<Explosion>().m_explosionSize += m_radiIncrease;
                    //bullet.GetComponent<ExplosiveBullet>().m_biggerExplosion.GetComponent<Explosion>().m_explosionSize += m_radiIncrease;
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
