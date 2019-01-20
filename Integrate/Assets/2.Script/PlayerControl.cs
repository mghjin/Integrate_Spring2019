﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {


    #region Player Parameters
    public float moveSpeed_nornal = 5.0f;
    public float moveSpeed_armed = 1.0f;
    public float jumpForce = 10.0f;
    public float maxHP = 100f;

    public GameObject ammoPrefab;
    #endregion

    #region References
    GameObject player;
    GameObject cannon;
    GameObject cannonFollowingPosition;
    GameObject firePoint;
    GameObject vfx_charging;
    Rigidbody rb;
    CapsuleCollider coll;
    #endregion

    #region Status
    public bool isAlive = true;

    public bool isSheathed = true; //is sheathed = cannot attack. 
    public bool isCharging = false;
    public float chargingSpeed = 50f;
    public float currentCharged;

    public bool canAttack = true;
    public bool canControl = true;
    public bool canMove = true;
    public bool canSheathe = true;
    public bool canShoot = true;
    public float currentHP = 1f;
    #endregion

    void Start () {
        //find references
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        player = this.gameObject;
        cannon = GameObject.Find("Cannon");
        cannonFollowingPosition = GameObject.Find("CannonFollowingPoint");
        firePoint = GameObject.Find("FirePoint");
        vfx_charging = GameObject.Find("VFX_charging");


        //initialization
        currentHP = maxHP;
        vfx_charging.SetActive(false);
        
	}
	

	void Update () {

        //check status


        //player control
        if (canControl)
        {
        
            Move();
            Aim(); // this deal with the action of cannon
            ChargeWeapon();
            Sheathe();
            Jump();


        }


    }

    private void Aim()
    {
        if (!isSheathed)
        {
            cannon.transform.position = new Vector3(Mathf.Lerp(cannon.transform.position.x, player.transform.position.x, 0.1f),
                                           Mathf.Lerp(cannon.transform.position.y, player.transform.position.y, 0.1f),
                                               cannon.transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            cannon.transform.rotation = rotation;
            cannon.transform.eulerAngles = new Vector3(0, 0, cannon.transform.eulerAngles.z);
        }
        else
        {
            cannon.transform.position = new Vector3(Mathf.Lerp(cannon.transform.position.x, cannonFollowingPosition.transform.position.x, 0.03f), 
                                                       Mathf.Lerp(cannon.transform.position.y, cannonFollowingPosition.transform.position.y, 0.03f), 
                                                           cannon.transform.position.z);
        }
    }

    private bool Groundcheck()
    {

        if (Physics.Raycast(player.transform.position,Vector3.down,coll.height/2+0.5f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Groundcheck() && canMove)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

        }

    }

    private void Sheathe()
    {
        if (canSheathe && Input.GetKeyDown(KeyCode.Q))
        {
            if (isSheathed)
            {
                isSheathed = false; //take out weapon
            }
            else
            {
                isSheathed = true; //take back weapon
                cannon.transform.rotation = Quaternion.identity;
            }
        }
    }

    private void ChargeWeapon()
    {
        if (canShoot)
        {
            if (Input.GetMouseButtonDown(0) && !isSheathed)
            {
                isCharging = true;
                if (!vfx_charging.gameObject.activeSelf)
                {
                    vfx_charging.SetActive(true);
                }

            }
            if (Input.GetMouseButtonUp(0) && !isSheathed)
            {
                isCharging = false;
                currentCharged = 0f;
                if (vfx_charging.gameObject.activeSelf)
                {
                    vfx_charging.SetActive(false);
                }
            }
            if (isCharging)
            {
                currentCharged += chargingSpeed * Time.deltaTime;
            }

            if (currentCharged >= 100f)
            {
                currentCharged = 0;
                isCharging = false;
                if (vfx_charging.gameObject.activeSelf)
                {
                    vfx_charging.SetActive(false);
                }
                Shoot();

            }
        }

    }

    private void Shoot()
    {
        Instantiate(ammoPrefab, firePoint.transform.position, cannon.transform.rotation);
    }

    private void Move()
    {
        float axis_x = Input.GetAxis("Horizontal");
        if (canMove)
        {
            if (isSheathed)
            {
                player.transform.position += Vector3.right * axis_x * moveSpeed_nornal * Time.deltaTime;
            }
            else
            {
                player.transform.position += Vector3.right * axis_x * moveSpeed_armed * Time.deltaTime;
            }
        }
    }
}