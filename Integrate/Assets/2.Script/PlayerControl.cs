/* 
 * GSND 6320 PSYCHOLOGY OF PLAY
 * PROJECT 1 DIGITAL PROTOTYPE
 * CODERS:
 * SIDAN FAN
 * JIN H KIM
 * 
 * EDITORS:
 * SONYA I MCCREE
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    // SFX collection that is emitted from player object
    public AudioSource cannon_startup,  //noise that plays upon left mouse click
        cannon_charge,                  //noise that plays while left mouse click
        cannon_shoot,                   //noise that plays upon weapon shoot (remove left click)
        health_station,                 //noise that plays upon picking up health
        jump_sfx;                       //noise that plays upon player jumping (pressing space)

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
    public HeroStatusPannelControl heroStatusPanelControl;
    public GameObject[] keyPos;
    Animator animator;
    LevelManager levelManager;

    #endregion
    #region Status
    public bool isAlive = true;
    public bool isFacingRight = true;
    public bool isInvincible = false;

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

    public int maxNumberOfKeysCarried = 3; // DO NOT Change the initial value casually!!!!
    public int numberOfKeysCarried = 0; // this variable will only be changed by external events.
    #endregion

    #region AI Status
    public bool chaosMechanicEnabled = false;
    public bool isChaos = false; //when true, it means the player cannot control but will attack people automatically.
    public GameObject automaticallyShootTarget;
    public EnemyControl automaticallyShootTargetEnemyControl;
    #endregion

    void Start()
    {
        //find references
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        player = this.gameObject;
        cannon = GameObject.Find("Cannon");
        cannonFollowingPosition = GameObject.Find("CannonFollowingPoint");
        firePoint = GameObject.Find("FirePoint");
        vfx_charging = GameObject.Find("VFX_charging");
        heroStatusPanelControl = GameObject.Find("PlayerStatusPanel").GetComponent<HeroStatusPannelControl>();
        animator = player.GetComponentInChildren<Animator>();


        //initialization
        currentHP = maxHP;
        vfx_charging.SetActive(false);

        levelManager = FindObjectOfType<LevelManager>();

        //initialization
        currentHP = maxHP;
        vfx_charging.SetActive(false);

        //Start Chaos Mechanic
        if (levelManager.rebelliousLevel >= 1 && chaosMechanicEnabled)
        {
            StartCoroutine("ChaosMechanicCoolDown");
        }

    }

    void Update()
    {
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

        if (chaosMechanicEnabled && isChaos)
        {
            ChaosAction();
        }
    }

    private void ChaosAction()
    {

        if (isSheathed)
        {
            isSheathed = false;
        }
        AutomaticallyAim();

        if (!isSheathed)
        {
            isCharging = true;
            if (!vfx_charging.gameObject.activeSelf)
            {
                cannon_startup.Play();
                cannon_charge.Play();
                vfx_charging.SetActive(true);
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

    private void AutomaticallyAim()
    // activates during chaos mode, where player cannot control
    // aims for the nearest virus object and charge/shoots
    {
        if (automaticallyShootTarget != null)
        {
            if (automaticallyShootTargetEnemyControl.currentHP <= 0.1f)
            {
                automaticallyShootTargetEnemyControl = null;
                automaticallyShootTarget = null;
            }

            // make sure to set reference for null and !null
            if (automaticallyShootTarget != null)
            {
                if ((automaticallyShootTarget.transform.position - transform.position).magnitude >= 7.0f)
                {
                    automaticallyShootTargetEnemyControl = null;
                    automaticallyShootTarget = null;
                }
            }

        }

        if (automaticallyShootTarget == null)
        {
            float distance = 10000f;
            GameObject[] objs;
            objs = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in objs)
            {
                automaticallyShootTargetEnemyControl = obj.GetComponent<EnemyControl>();
                if (automaticallyShootTargetEnemyControl.currentHP >= 0.1f)
                {
                    float dist = (obj.transform.position - transform.position).magnitude;
                    if (dist <= distance)
                    {
                        distance = dist;
                        automaticallyShootTarget = obj;
                    }
                }
            }
            automaticallyShootTargetEnemyControl = automaticallyShootTarget.GetComponent<EnemyControl>();
        }

        if (!isSheathed)
        {
            cannon.transform.position = new Vector3(Mathf.Lerp(cannon.transform.position.x, player.transform.position.x, 0.1f),
                                           Mathf.Lerp(cannon.transform.position.y, player.transform.position.y + 0.2f, 0.1f),
                                               cannon.transform.position.z);

            Quaternion rotation = Quaternion.LookRotation(transform.position - automaticallyShootTarget.transform.position + Vector3.forward * (10f), Vector3.forward);
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log(automaticallyShootTarget.transform.position + Vector3.forward * (-10f));
            cannon.transform.rotation = rotation;
            cannon.transform.eulerAngles = new Vector3(0, 0, cannon.transform.eulerAngles.z);

        }

    }


    IEnumerator ChaosMechanicCoolDown()
    {
        isChaos = false;
        canControl = true;
        isCharging = false;
        currentCharged = 0f;
        if (vfx_charging.gameObject.activeSelf)
        {
            cannon_charge.Stop();
            vfx_charging.SetActive(false);
        }
        float coolDownTime;
        //coolDownTime = 60 - 10 * UnityEngine.Random.Range(1,levelManager.rebelliousLevel);
        coolDownTime = 5f;
        yield return new WaitForSeconds(coolDownTime);
        isChaos = true;
        canControl = false;
        coolDownTime = 4 + levelManager.rebelliousLevel * 1.5f;
        yield return new WaitForSeconds(coolDownTime);
        StartCoroutine("ChaosMechanicCoolDown");


    }

    private void Aim()
    {
        //player can aim the direction of the cannon IFF the cannon is unsheathed
        if (!isSheathed)
        {
            cannon.transform.position = new Vector3(Mathf.Lerp(cannon.transform.position.x, player.transform.position.x, 0.1f),
                                           Mathf.Lerp(cannon.transform.position.y, player.transform.position.y + 0.2f, 0.1f),
                                               cannon.transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            cannon.transform.rotation = rotation;
            cannon.transform.eulerAngles = new Vector3(0, 0, cannon.transform.eulerAngles.z);
        }
        else
        // if weapon is sheathed, its sprite will follow the player object
        {
            cannon.transform.position = new Vector3(Mathf.Lerp(cannon.transform.position.x, cannonFollowingPosition.transform.position.x, 0.08f),
                                                       Mathf.Lerp(cannon.transform.position.y, cannonFollowingPosition.transform.position.y, 0.08f),
                                                           cannon.transform.position.z);
        }
    }

    private bool Groundcheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position + Vector3.right * 0.26f, Vector3.down, out hit, coll.height / 2 + 0.5f) || Physics.Raycast(player.transform.position + Vector3.left * 0.26f, Vector3.down, out hit, coll.height / 2 + 0.5f))
        {
            //checks whether the player is on a solid platform
            if (hit.collider.gameObject.layer == 9)
            {
                return (true);
            }
            return false;
        }
        else
        //if player is "floating"/not on a solid platform, return false
        {
            return false;
        }
    }

    private void Jump()
    {
        // checks if the player presses the space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if spacedown == true and the player is not in the air, JUMP
            if (Groundcheck() && canMove)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jump_sfx.Play();
            }
        }
    }

    private void Sheathe()
    {
        // if the weapon is out, the player can sheathe weapon
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
            //left click while weapon is unsheathed
            if (Input.GetMouseButtonDown(0) && !isSheathed)
            {
                isCharging = true;
                if (!vfx_charging.gameObject.activeSelf)
                {
                    cannon_startup.Play();
                    cannon_charge.Play();
                    vfx_charging.SetActive(true);
                }
            }
            //cancelling left click
            if (Input.GetMouseButtonUp(0) && !isSheathed)
            {
                isCharging = false;
                currentCharged = 0f;
                if (vfx_charging.gameObject.activeSelf)
                {
                    cannon_charge.Stop();
                    vfx_charging.SetActive(false);
                }
            }
            if (isCharging)
            //charges weapon for a duration; can be cancelled with left click
            {
                currentCharged += chargingSpeed * Time.deltaTime;
            }

            if (currentCharged >= 100f)
            {
                //once fully charged, call the shoot function
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
        //plays shooting sfx and fires the bullet in direction the player is aiming
        cannon_charge.Stop();
        cannon_shoot.Play();
        Instantiate(ammoPrefab, firePoint.transform.position, cannon.transform.rotation);
    }

    private void Move()
    {
        float axis_x = Input.GetAxis("Horizontal");
        //animator control
        if (axis_x > 0.1f || axis_x < -0.1f)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        //facing control
        if (axis_x > 0)
        {
            //turning the character and weapon sprites left and right according to player input
            if (!isFacingRight)
            {
                isFacingRight = true;
                player.transform.localScale = new Vector3(-player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z);
                cannon.transform.localScale = new Vector3(-cannon.transform.localScale.x, cannon.transform.localScale.y, cannon.transform.localScale.z);
            }
        }
        else if (axis_x < 0)
        {
            if (isFacingRight)
            {
                isFacingRight = false;
                player.transform.localScale = new Vector3(-player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z);
                cannon.transform.localScale = new Vector3(-cannon.transform.localScale.x, cannon.transform.localScale.y, cannon.transform.localScale.z);
            }
        }
        //move
        if (canMove)
        {
            if (isSheathed)
            {
                //if weapon is sheathed, player moves at "normal" speed which is faster
                player.transform.position += Vector3.right * axis_x * moveSpeed_nornal * Time.deltaTime;
            }
            else
            {
                //if weapon is unsheathed, player moves at "armed" speed which is slower
                player.transform.position += Vector3.right * axis_x * moveSpeed_armed * Time.deltaTime;
            }
        }
    }

    public void GetHit(float damage)  //call this function to deal damage to the hero
    {
        if (!isInvincible)
        {
            currentHP -= damage;
            heroStatusPanelControl.RefreshHPBarDisplay();
            DeathCheck();
        }
    }

    public void DeathCheck()
    {
        if (currentHP <= 0 && isAlive)
        {
            //once health parameter reaches zero, player character dies
            //and cannot be controlled
            canControl = false;
            isAlive = false;
        }
    }
}



