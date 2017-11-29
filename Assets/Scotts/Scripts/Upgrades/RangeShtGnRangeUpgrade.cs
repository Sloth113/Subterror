using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeShtGnRangeUpgrade : iUpgrade {
    public float m_rangeIncrease = 5.0f;

    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            foreach (GameObject bullet in playerScript.m_bulletPrefabs) {
                if (bullet.GetComponent<ShotgunShot>() != null) {
                    bullet.GetComponent<ShotgunShot>().m_bullets.GetComponent<ShotgunBullet>().lifeTime += m_rangeIncrease;
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
