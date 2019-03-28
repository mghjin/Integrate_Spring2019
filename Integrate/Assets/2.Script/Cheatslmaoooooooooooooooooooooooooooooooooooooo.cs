using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheatslmaoooooooooooooooooooooooooooooooooooooo : MonoBehaviour
{
    PlayerControl playerControl;
    [SerializeField] GameObject keyPrefab;   //drag for reference


    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.N) && Input.GetKeyDown(KeyCode.U)) || (Input.GetKey(KeyCode.U) && Input.GetKeyDown(KeyCode.N)))
        {
            Instantiate(keyPrefab, playerControl.gameObject.transform.position + Vector3.right * 2, Quaternion.identity);
            Instantiate(keyPrefab, playerControl.gameObject.transform.position + Vector3.right * 3, Quaternion.identity);
            Instantiate(keyPrefab, playerControl.gameObject.transform.position + Vector3.right * 4, Quaternion.identity);
        }
    }
}
