using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerTest : MonoBehaviour {
    private CharacterController controller;
    public float speed = 10;
    public float shootCooldown = 5;
    private float shootTimer  =0;
    public GameObject bulletTest;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Input
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;

        if (!controller.isGrounded) {
            //fall
            move.y += Physics.gravity.y * 10 *  Time.fixedDeltaTime;
        } else {
            move.y = 0;
        }

        //Move
        controller.Move(move * Time.fixedDeltaTime);

        //Shoot
        if(Input.GetButton("Fire1") && shootTimer >= shootCooldown && bulletTest != null) {
            Instantiate<GameObject>(bulletTest, this.transform.position, this.transform.rotation);//make transform postition the point on the gun
            shootTimer = 0;
        }else {
            shootTimer += Time.deltaTime;
        }
        
    }
}
