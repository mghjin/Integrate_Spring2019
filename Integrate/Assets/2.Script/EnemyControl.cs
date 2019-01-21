using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {


    #region Parameter
    enum enemyType { Man, Woman, Hero, Child, Flower};
    [SerializeField] enemyType typeOfThis;
    [SerializeField] float maxHP = 100f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float alertRange = 10f;
    [SerializeField] float attackDamage = 5f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] float attackHPCost = 35f;
    #endregion

    #region Status
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool isAlive = true;
    [SerializeField] bool canMove = true;
    [SerializeField] bool isAlert = false;
    [SerializeField] bool isAggressive = false;
    [SerializeField] bool canAttack = true;

    [SerializeField] float currentHP = 1f;
    #endregion

    #region Reference
    Rigidbody rb;
    PlayerControl playerControl;
    #endregion

    void Start () {
        //Find Reference
        rb = GetComponent<Rigidbody>();
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();

        //Initialization
        currentHP = maxHP;
	}
	

	void Update () {
        if (isAlive)
        {
            if (canMove)
            {
                FacingCheck();
                Move();
            }

        }

	}

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

    private void Attack()
    {

    }
}
