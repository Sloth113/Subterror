using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour {
    public GameObject m_explosion;
    public GameObject m_biggerExplosion;
    public float speed = 10;
    public float lifeTime = 1;
    private float timer = 0;
    public float m_damage = 10;

    public bool m_betterExplosion;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Move forward
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
        if (timer >= lifeTime) {
            if (m_explosion != null)
                if (m_betterExplosion) {
                    Instantiate<GameObject>(m_biggerExplosion, this.transform.position, this.transform.rotation);
                }else {
                    Instantiate<GameObject>(m_explosion, this.transform.position, this.transform.rotation);
                }
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
        if (m_betterExplosion) {
            Instantiate<GameObject>(m_biggerExplosion, this.transform.position, this.transform.rotation);
        } else {
            Instantiate<GameObject>(m_explosion, this.transform.position, this.transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
