/* 
 * GSND 6320 PSYCHOLOGY OF PLAY
 * PROJECT 1 DIGITAL PROTOTYPE
 * CODERS/EDITORS:
 * SIDAN FAN
 * JIN H KIM
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour {

    // SFX that is emitted from the gate object within a level
    public AudioSource gate_open_sfx;

    [SerializeField] int numberOfKeysRequired = 3; //DO NOT change this.
                                                    // player MUST have 3 keys in order to open the gate
    [SerializeField] int numberOfKeysCaptured = 0;
    [SerializeField] GameObject[] keyPos;
    [SerializeField] GameObject laserDoor;          //solid boundary player cannot pass when locked
    [SerializeField] PlayerControl playerControl;

	void Start () {
        //search for player object to see if it is near the gate object
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key" && numberOfKeysCaptured < numberOfKeysRequired)
        {
            KeyControl keyControl = other.GetComponent<KeyControl>();
            keyControl.isInsertedToGate = true;
            keyControl.followPoint = keyPos[numberOfKeysCaptured];
            numberOfKeysCaptured++;
            playerControl.numberOfKeysCarried--;

            if (numberOfKeysCaptured >= numberOfKeysRequired)
            //if player has 3 keys that have been set into gate
            //set laserDoor false (i.e open the gate) and play sfx
            {
                laserDoor.SetActive(false);
                gate_open_sfx.Play();
            }
        }
    }
}
