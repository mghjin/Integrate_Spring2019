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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousArea : MonoBehaviour
{

    public AudioSource glitcharea_sfx;  // plays while the player object is colliding with area

    public bool isHurting = true;
    public float collDownTime = 0.5f;
    public float damagePerTimeUnit = 5;
    [SerializeField] PlayerControl playerControl;

  

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }

    // on trigger enter 
    // tests whether player is in area and loops sfx
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
            glitcharea_sfx.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
            glitcharea_sfx.Stop();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if (playerControl.isAlive && isHurting)
            {
                playerControl.GetHit(damagePerTimeUnit);
                StartCoroutine("CoolDown");
            }
        }
    }

    IEnumerator CoolDown()
    {
        isHurting = false;
        yield return new WaitForSeconds(collDownTime);
        isHurting = true;
    }
}
