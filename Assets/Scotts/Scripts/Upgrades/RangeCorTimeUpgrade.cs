using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RangeCorTimeUpgrade : iUpgrade {
    public float m_timeIncreaseAmount = 3.0f;

    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            foreach (GameObject bullet in playerScript.m_bulletPrefabs) {
                if (bullet.GetComponent<CorrosiveBullet>() != null) {
                    bullet.GetComponent<CorrosiveBullet>().m_corrsiveEffect.GetComponent<CorrosiveEffect>().m_duration += m_timeIncreaseAmount;
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
