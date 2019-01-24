using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour {


    [SerializeField] int numberOfKeysRequired = 3; //DO NOT change this.
    [SerializeField] int numberOfKeysCaptured = 0;
    [SerializeField] GameObject[] keyPos;
    [SerializeField] GameObject laserDoor;
    [SerializeField] PlayerControl playerControl;


	void Start () {
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
            {
                laserDoor.SetActive(false);
            }
        }
    }


}
