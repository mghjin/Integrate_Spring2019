using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStationControl : MonoBehaviour {

    [SerializeField] GameObject triggerText;
    [SerializeField] GameObject VFX_healthStation;
    [SerializeField] GameObject VFX_HealingPrefab;
    [SerializeField] float healingValue = 100;
    [SerializeField] bool isTriggered = false;

    [SerializeField] PlayerControl playerControl;

    void Start () {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player" && !isTriggered)
        {
            isTriggered = true;
            playerControl.currentHP += healingValue;
            if (playerControl.currentHP > playerControl.maxHP)
            {
                playerControl.currentHP = playerControl.maxHP;
            }
            GameObject temp_vfx = Instantiate(VFX_HealingPrefab, transform.position, Quaternion.identity);
            Destroy(temp_vfx, 2.0f);
            VFX_healthStation.SetActive(false);
        }
    }

}
