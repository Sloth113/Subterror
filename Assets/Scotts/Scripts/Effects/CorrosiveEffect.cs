using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Effect that will hurt the attached target
public class CorrosiveEffect : MonoBehaviour {
    private iHitable m_attached;
    public float m_duration = 2.0f;
    public int m_damage = 5;
    public float m_damRate = 1.0f;
    private float m_timer = 0;
    public GameObject m_effect;
    private GameObject m_spawn;
	// Use this for initialization
	void Start () {
        m_attached = GetComponent<iHitable>();
        if (m_effect != null) {
            m_spawn = Instantiate<GameObject>(m_effect, transform.position, transform.rotation);
            m_spawn.transform.parent = this.transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;
        if(m_timer > m_damRate) {
            m_attached.Hit(m_damage);
            m_damRate += m_damRate;
        }
        if(m_timer > m_duration) {
            Destroy(m_spawn);
            Destroy(this);
        }
	}
    
}
