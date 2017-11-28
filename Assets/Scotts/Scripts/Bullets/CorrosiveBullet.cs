using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrosiveBullet : MonoBehaviour {
    public GameObject m_corrsiveEffect;
    public float speed = 10;
    public float lifeTime = 1;
    private float timer = 0;
    public float m_damage = 10;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Move forward
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
        if (timer >= lifeTime) {
            Destroy(this.gameObject);
        } else {
            timer += Time.deltaTime;
        }
    }


    void OnTriggerEnter(Collider c) {
        iHitable script = c.GetComponent<iHitable>();
        if (script != null) {
            script.Hit((int)m_damage);
        }
        //Effect
        //
        if (m_corrsiveEffect != null && c.gameObject.GetComponent<iHitable>() != null) {
            c.gameObject.AddComponent<CorrosiveEffect>();
            c.gameObject.GetComponent<CorrosiveEffect>().m_effect = m_corrsiveEffect;
        }
        Destroy(this.gameObject);
    }
}
