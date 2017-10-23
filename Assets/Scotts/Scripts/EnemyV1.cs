using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyV1 : MonoBehaviour {
    public int hp = 3;
    public float moveSpeed = 3.0f;
    //
    public GameObject target;
    public float distToTarget = 0;
    public bool melee = true;
    public bool ranged = false;
    //
    public float meleeDamage = 1.0f;
    public float rangeDamage = 0.0f;
    public float meleeCooldown = 1.0f;//Seconds
    public float rangedCooldown = 5.0f;
    private float meleeTimer = 0.0f;
    private float rangedTimer = 0.0f;
    //
    private CharacterController controller;

    // Use this for initialization
    void Start () {
        controller = this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        //Move parts
		if(target != null) {
            //Move towards
            if((target.transform.position - this.transform.position).magnitude > distToTarget) {
                Vector3 dir = target.transform.position - this.transform.position;
                dir.Normalize();
                if (!controller.isGrounded) {
                    //fall
                    dir.y += Physics.gravity.y * 10 * Time.fixedDeltaTime;
                } else {
                    dir.y = 0;
                }
                controller.Move(dir * moveSpeed * Time.deltaTime);
            } 
        }

        //Attack parts
        //Melee
        if (melee && meleeTimer > meleeCooldown && (target.transform.position - this.transform.position).magnitude <1.5f) {
            //Melee attack
            Debug.Log("Melee Attack");
            meleeTimer = 0.0f;
        }else {
            meleeTimer += Time.deltaTime;
        }
        //Ranged
        if (ranged && rangedTimer > rangedCooldown && (target.transform.position - this.transform.position).magnitude > 2.0f) {
            //Range attack
            Debug.Log("Range Attack");
            rangedTimer = 0.0f;
        } else {
            rangedTimer += Time.deltaTime;
        }
        
    }
}
