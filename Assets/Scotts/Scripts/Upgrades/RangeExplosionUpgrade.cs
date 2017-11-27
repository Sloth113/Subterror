using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeExplosionUpgrade : iUpgrade {
    public GameObject m_newExplosionSize; 

    public override void Apply(GameObject player) {
        CharacterControllerTest playerScript = player.GetComponent<CharacterControllerTest>();
        if (playerScript != null) {
            //playerscript.bullets.add(corrsive);
        }
    }

    public override bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades) {
        if (inv.mutagen > m_info.cost) {
            return true;
        }
        return false;
    }
}
