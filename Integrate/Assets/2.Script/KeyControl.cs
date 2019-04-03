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

public class KeyControl : MonoBehaviour {

    // like player's cannnon,
    // the keys are floating objects set to follow player location
    public GameObject followPoint;
    [SerializeField] bool isPickedUp = false;
    public bool isInsertedToGate = false;

    [SerializeField] GameObject player;
    [SerializeField] PlayerControl playerControl;

    void Start () {
        //find references
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
	}

    // floating object is picked up upon player collision
    // will not follow unless actively picked up by player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !isPickedUp && !isInsertedToGate)
        {
            if (playerControl.numberOfKeysCarried < playerControl.maxNumberOfKeysCarried)
            {
                isPickedUp = true;
                playerControl.numberOfKeysCarried++;
                followPoint = playerControl.keyPos[playerControl.numberOfKeysCarried - 1];
            }
        }
    }

    // update position to follow player
    private void Update()
    {
        if (followPoint != null)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followPoint.transform.position.x, 0.05f), 
                                              Mathf.Lerp(transform.position.y, followPoint.transform.position.y, 0.05f), 
                                               transform.position.z);
        }

    }

}
