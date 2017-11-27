using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MeleeAOEUpgrade : iUpgrade {
    public int m_aoeSize = 10;

    public override void Apply(GameObject player) {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null) {
           //Toggle on player
        }
    }

    public override bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades) {
        if (inv.mutagen > m_info.cost) {
            return true;
        }
        return false;
    }
}
