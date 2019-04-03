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

public class ChaosTrigger : MonoBehaviour
{

    //parameters
    [SerializeField] int rebelliosLevelRequired = 1;
    [SerializeField] float chaoticTime = 5f;
    
    //reference
    [SerializeField] PlayerControl playerControl;
    [SerializeField] LevelManager levelManager;

    //Status
    [SerializeField] bool hasBeenTriggered = false;

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        StartCoroutine(FindLevelManager());
    }

    IEnumerator FindLevelManager()
    {
        yield return new WaitForSeconds(0.5f);
        levelManager = FindObjectOfType<LevelManager>();
    }

    //depending on the player's rebellious level
    //and the level they are currently in
    //provide a possibility that the player enters chaos mode
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player" && !hasBeenTriggered)
        {
            if (levelManager.rebelliousLevel >= rebelliosLevelRequired)
            {
                hasBeenTriggered = true;
                StartCoroutine(StartChaoticBehavior());
            }
        }
    }
    
    //chaos mechanic triggers and automatically shuts off after a given duration
    IEnumerator StartChaoticBehavior()
    {
        playerControl.StartCoroutine("StartChaosMechanic");
        yield return new WaitForSeconds(chaoticTime);
        playerControl.StartCoroutine("StopChaosMechanic");
    }
}
