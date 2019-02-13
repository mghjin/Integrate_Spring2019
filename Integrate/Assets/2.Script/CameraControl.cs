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

public class CameraControl : MonoBehaviour {

    GameObject player;
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, 0.05f), Mathf.Lerp(transform.position.y, player.transform.position.y, 0.05f), transform.position.z);
	}
}
