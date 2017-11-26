using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCorrosionUpgrade : MonoBehaviour, iUpgrade {
    public GameObject m_corrosiveBullet;
    public upgradeDetails m_info;
    public string m_details = "Add corrosive projectile to players range choice";

    public void Apply(GameObject player) {
        CharacterControllerTest playerScript = player.GetComponent<CharacterControllerTest>();
        if (playerScript != null) {
            //playerscript.bullets.add(corrsive);
        }
    }
    public upgradeDetails CostAmount() {
        return m_info;
    }

    public string GetDetails() {
        return m_details + m_info.cost + " scrap";
    }
    public void DebugInfo() {
        Debug.Log(GetDetails());
    }

    public bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades) {
        if (inv.mutagen > m_info.cost) {
            return true;
        }
        return false;
    }
}
