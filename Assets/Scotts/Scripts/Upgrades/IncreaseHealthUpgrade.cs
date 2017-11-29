using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IncreaseHealthUpgrade : iUpgrade {
    public int m_increaseAmount = 10;
    public Image m_progressBar;

    public override void Apply(GameObject player) {
        CharacterControllerTest playerScript = player.GetComponent<CharacterControllerTest>();
        if(playerScript != null) {
            playerScript.m_hp += m_increaseAmount;
            playerScript.maxHp += m_increaseAmount;
        }
    }

    public override bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades)
    {
        if (inv.mutagen >= m_info.cost && m_progressBar.fillAmount != 1)
        {
            m_progressBar.fillAmount += .1f; //Is here because only applys after this is works
            return true;
        }
        return false;
    }
    public new void AddToPlayer()
    {
        base.AddToPlayer();
    }
}
