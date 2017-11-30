using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script put onto effects that have set duration
public class EffectDestroy : MonoBehaviour {

    public float m_duration = 2.0f;
    private float m_timer = 0;

    void Update() {
        m_timer += Time.deltaTime;
        if (m_timer > m_duration) {
            Destroy(this.gameObject);
        }
    }
}
