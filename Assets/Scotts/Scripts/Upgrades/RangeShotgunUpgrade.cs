using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RangeShotgunUpgrade : iUpgrade {
    public GameObject m_shotgunPrefab;  

    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
            playerScript.m_bulletPrefabs.Add(m_shotgunPrefab);
        }
    }
    public override bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades) {
        if (inv.scrap > m_info.cost) {
            return true;
        }
        return false;
    }

}
