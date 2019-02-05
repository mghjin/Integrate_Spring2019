using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousArea : MonoBehaviour
{
    public bool isHurting = true;
    public float collDownTime = 0.5f;
    public float damagePerTimeUnit = 5;
    [SerializeField] PlayerControl playerControl;

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
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
