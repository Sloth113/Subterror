﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerTest : MonoBehaviour {

    private CharacterController controller;

    //Stats
    public float speed = 10;
    public float hp = 100;
    public float maxHp = 100;
    public float incomeDamMod = 1.0f;
    private float m_MAXSPEED = 5;
    
    //Melee
    public float meleeCooldown = 0.5f;
    private float meleeTimer = 0;
    public float meleeMod = 1.0f;
    public float meleeRange = 1.0f;
    public float meleeAngle = 90; //Degrees

    //Ranged
    public float shootCooldown = 5;
    private float shootTimer  =0;
    public GameObject bulletTest;

    //Inventory
    private List<string> m_keys;
    public int m_scrap =0;
    public int m_mutagen =0;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        m_keys = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
        //Input
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
        if(move.sqrMagnitude >= m_MAXSPEED*m_MAXSPEED) {
            move = move.normalized * m_MAXSPEED;
        }
        transform.LookAt(getMouseToPlayerPlanePoint()); //look at mouse

        //Keep grounded
        if (!controller.isGrounded) {
            //fall
            move.y += Physics.gravity.y * 10 *  Time.deltaTime;
        } else {
            move.y = 0;
        }

        //Move
        controller.Move(move * Time.deltaTime);

        //Shoot
        if(Input.GetButton("Fire1") && shootTimer >= shootCooldown && bulletTest != null) {
            Instantiate<GameObject>(bulletTest, this.transform.position, this.transform.rotation);//make transform postition the point on the gun
            shootTimer = 0;
        }//Melee
        else if(Input.GetButton("Fire2") && meleeTimer >= meleeCooldown) {
            //Sphere case in front?
            Debug.Log("Melee");
            //
            //RaycastHit hit;

            //
            meleeTimer = 0;
        }
        //Timers
        if (shootTimer < shootCooldown) {
            shootTimer += Time.deltaTime;
        }
        if (meleeTimer < meleeCooldown) {
            meleeTimer += Time.deltaTime;
        }
        
    }


    //Used to use mouse on board
    private Vector3 getMouseToPlayerPlanePoint() {

        Vector3 mousePos = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

        Plane playersYPlane = new Plane(Vector3.up, transform.position);
        float rayDist = 0;

        playersYPlane.Raycast(mouseRay, out rayDist);

        Vector3 castPoint = mouseRay.GetPoint(rayDist);
        return castPoint;
    }

    void OnTriggerEnter(Collider c) {
        //Items
        if(c.transform.tag == "Item") {
            string itemName = c.GetComponent<ItemScriptOne>().m_info;
            if (itemName.Contains("Key")){
                m_keys.Add(itemName);
            }else if(itemName == "Scrap") {
                m_scrap++;
            }else if(itemName == "MutaGen") {
                m_mutagen++;
            }
            Destroy(c.gameObject);
        }
    }
}
