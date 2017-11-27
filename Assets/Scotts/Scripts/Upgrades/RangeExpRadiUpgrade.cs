using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RangeExpRadiUpgrade : iUpgrade {
    public float m_radiIncrease; //

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
