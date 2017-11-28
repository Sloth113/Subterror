using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//Should of made a class
[System.Serializable]
public enum upgradeType {
    MutaGen,
    Scrap
}
[System.Serializable]
public struct upgradeDetails {
    public int cost;
    public upgradeType type;
}
public abstract class iUpgrade : MonoBehaviour, ISelectHandler {
  
    public upgradeDetails m_info;
    public string m_details;
    public Text m_infoText;
    public Button m_nextUnlock;

    abstract public void Apply(GameObject player);
    
    abstract public bool PreRequisteMet(Inventory inv, List<iUpgrade> upgrades);

    public void AddToPlayer() {
        if (PreRequisteMet(GameManager.Instance.PlayersInventory(), GameManager.Instance.PlayersUpgrades())) {
            GameManager.Instance.AddUpgrade(this);
            this.GetComponent<Button>().interactable = false;
            if(m_nextUnlock != null) {
                m_nextUnlock.interactable = true;
            }
        }
    }
    public string GetDetails() {
        return m_details;
    }

    public upgradeDetails CostAmount() {
        return m_info;
    }

    public void OnSelect(BaseEventData eventData) {
        m_infoText.text = GetDetails();
    }
    
}
