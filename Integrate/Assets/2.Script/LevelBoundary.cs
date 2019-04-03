/* 
 * GSND 6320 PSYCHOLOGY OF PLAY
 * PROJECT 1 DIGITAL PROTOTYPE
 * CODERS:
 * SIDAN FAN
 * JIN H KIM 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    [SerializeField] PlayerControl playerControl;
    [SerializeField] GameObject vfx_death; //drag the prefab!

    private void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // boundaries set around each level
    // ensures that players do not fall endlessly
    // triggers a death and level restart,
    // where all level progress must be redone
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Instantiate(vfx_death, playerControl.gameObject.transform.position, Quaternion.identity);
            playerControl.canControl = false;
            playerControl.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            playerControl.gameObject.GetComponent<Collider>().enabled = false;
            playerControl.ReloadFromDeath();
            
        } 
    }
}
