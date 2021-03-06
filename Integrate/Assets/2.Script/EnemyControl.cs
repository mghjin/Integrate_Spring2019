﻿/* 
 * GSND 6320 PSYCHOLOGY OF PLAY
 * PROJECT 1 DIGITAL PROTOTYPE
 * CODERS:
 * SIDAN FAN
 * JIN H KIM 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {


    #region Parameter
    public bool dontCountWhenCalculatingTotalNumberOfEnemies = false;
    enum enemyType { Man, Woman, Hero, Child, Flower};
    [SerializeField] enemyType typeOfThis;
    [SerializeField] float maxHP = 100f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float alertRange = 10f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] float attackHPCost = 35f;
    [SerializeField] float attackCoolDown = 2f;
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] GameObject firePos;
    [SerializeField] GameObject dropPrefab;
    [SerializeField] bool willDropKey = false;
    #endregion

    #region Status
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool isAlive = true;
    [SerializeField] bool canMove = true;
    [SerializeField] bool isAlert = false;
    [SerializeField] bool isAggressive = false;
    [SerializeField] bool canAttack = true;
    [SerializeField] float currentAttackCoolDown = 0f;

    public float currentHP = 1f;
    #endregion

    #region Reference
    public GameObject[] relatives;
    Rigidbody rb;
    Collider coll;
    PlayerControl playerControl;
    [SerializeField] GameObject vfx_error;
    LevelManager levelmanager;
    Animator animator;
    #endregion

    void Start () {
        //Find Reference
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        animator = GetComponentInChildren<Animator>();


        //Initialization
        currentHP = maxHP;
	}
	
    // checks for current player status
	void Update () {
        if (isAlive)
        {
            if (canMove)
            {
                FacingCheck();
                Move();
                CoolDown();
            }
        }
	}

    // delay between the enemy's projectile attacks
    private void CoolDown()
    {
        if (currentAttackCoolDown >= 0)
        {
            currentAttackCoolDown -= Time.deltaTime;
        }
        else 
        {
            canAttack = true;
        }
    }

    // facing the player object
    // if has been attacked or hostile-action triggered, attack the player
    // hostility is triggered when a linked enemy object is attacked/killed by player
    private void FacingCheck()
    {
        if (!isAlert || isAggressive)
        {
            if (transform.position.x > playerControl.gameObject.transform.position.x)
            {
                if (isFacingRight)
                {
                    isFacingRight = false;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else
            {
                if (!isFacingRight)
                {
                    isFacingRight = true;
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }

        // initially not aggressive, simply turns to face the player while so
        if (isAlert && !isAggressive)
        {
            if (transform.position.x > playerControl.gameObject.transform.position.x)
            {
                if (!isFacingRight)
                {
                    isFacingRight = true;
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                if (isFacingRight)
                {
                    isFacingRight = false;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }

    }

    // moves towards player
    private void Move()
    {
        float distanceToPlayer = (transform.position - playerControl.gameObject.transform.position).magnitude;

        //Alert
        if ((!playerControl.isSheathed) && (distanceToPlayer <= alertRange))
        {
            if (!isAlert)
            {
                isAlert = true;
            }
        }
        else if (playerControl.isSheathed && !isAggressive)
        {
            if (isAlert)
            {
                isAlert = false;
            }
        }

        // if player unsheathes cannon, initial reaction is to run from player
        //Escape from player
        if (distanceToPlayer <= alertRange && isAlert && !isAggressive)
        {
            if (isFacingRight)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
        }

        // will chase player is hostility has been triggered
        //Chasing player
        if (distanceToPlayer <= alertRange && distanceToPlayer >= attackRange && isAggressive)
        {
            if (isFacingRight)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
        }

        //Attack player
        if (distanceToPlayer <= attackRange && isAggressive)
        {
            if (canAttack)
            {
                Attack();
            }

        }
    }

    // enemy object can only attack when it has sufficient HP
    // if not enough HP, the attack will also kill the enemy
    private void Attack()
    {
        canAttack = false;
        currentAttackCoolDown += attackCoolDown;
        currentHP -= attackHPCost;
        Instantiate(ammoPrefab,firePos.transform.position,Quaternion.identity);
        DeathCheck();
    }

    public void GetHit(float damage)  //call this function to deal damage to the hero
    {
        currentHP -= damage;
        DeathCheck();
    }

    // if linked enemy dies, trigger hostility in other linked enemy objects
    private void DeathCheck()
    {
        if (currentHP <= 0 && isAlive)
        {
            isAlive = false;
            canMove = false;
            rb.isKinematic = true;
            rb.useGravity = false;
            coll.enabled = false;
            EvokeAggressivenessOfRelatives();
            vfx_error.SetActive(false);
            levelmanager.calculateDeathOfEnemies();
            animator.SetBool("IsDead", true);
            if (willDropKey)
            {
                Instantiate(dropPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    // trigger hostility of linked enemy objects
    private void EvokeAggressivenessOfRelatives()
    {
        foreach (GameObject relative in relatives)
        {
            if (relative != null)
            {
                EnemyControl relativeControl = relative.GetComponent<EnemyControl>();
                if (relativeControl.isAlive)
                {
                    relativeControl.isAggressive = true;
                    relativeControl.vfx_error.SetActive(true);
                }
            }
        }
    }
}
