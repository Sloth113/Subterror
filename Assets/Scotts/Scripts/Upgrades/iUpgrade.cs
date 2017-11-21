using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public interface iUpgrade {
    void Apply(GameObject player);
    string GetDetails();
    upgradeDetails CostAmount();
}
