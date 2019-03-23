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
        levelManager = FindObjectOfType<LevelManager>();

    }


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

    IEnumerator StartChaoticBehavior()
    {
        playerControl.StartCoroutine("StartChaosMechanic");
        yield return new WaitForSeconds(chaoticTime);
        playerControl.StartCoroutine("StopChaosMechanic");
    }
}
