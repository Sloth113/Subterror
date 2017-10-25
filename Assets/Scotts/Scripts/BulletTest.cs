using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour {
    public float speed = 10;
    public float lifeTime = 1;
    private float timer = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Move forward
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
        if(timer >= lifeTime) {
            Destroy(this.gameObject);
        }else {
            timer += Time.deltaTime;
        }
	}
}
