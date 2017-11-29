using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeShtGBulletUpgrade : iUpgrade {
    public int m_newShotCount = 5;

    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            foreach (GameObject bullet in playerScript.m_bulletPrefabs) {
                if (bullet.GetComponent<ShotgunShot>() != null) {
                    bullet.GetComponent<ShotgunShot>().m_shotCount = m_newShotCount;
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
