using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShot : MonoBehaviour {
    public int m_shotCount = 3;
    public float m_spreadAngle = 30;
    public GameObject m_bullets;
	// Use this for initialization
	void Start () {
		if(m_bullets != null) {
            int i = 0;
            Transform currentPos = transform;
            currentPos.Rotate(0, -m_spreadAngle/2, 0);
            //Create from left to right
            while (i < m_shotCount) {
                Instantiate<GameObject>(m_bullets, currentPos.position, currentPos.rotation);
                currentPos.Rotate(0, m_spreadAngle / m_shotCount, 0);
                i++;
            }
        }
        Destroy(this.gameObject);
	}
	
}
