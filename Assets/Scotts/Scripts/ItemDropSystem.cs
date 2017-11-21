using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSystem : MonoBehaviour {
    [System.Serializable]
    public struct Drop {
        public GameObject item;
        public float dropRate;
    }
    public List<Drop> drops;

    public void DropItems() {
        DropItems(Random.Range(0, 100));
    }
    public void DropItems(float roll) {
        foreach(Drop d in drops) {
            float dropNum = d.dropRate;
            while(dropNum > 0) {
                //Drop if roll is higher than num or 
                if (roll < dropNum) {
                    Instantiate<GameObject>(d.item, this.transform.position + transform.up /2, this.transform.rotation);// spawn on location
                }
                dropNum -= 100;
            }
        }
    }
}
