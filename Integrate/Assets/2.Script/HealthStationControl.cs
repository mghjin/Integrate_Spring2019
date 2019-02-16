/* 
 * GSND 6320 PSYCHOLOGY OF PLAY
 * PROJECT 1 DIGITAL PROTOTYPE
 * CODERS:
 * SIDAN FAN
 * JIN H KIM
 * 
 * EDITORS:
 * SONYA I MCCREE
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStationControl : MonoBehaviour {

    public AudioSource healthpack_sfx; //sfx that plays upn player collision with health pack

    [SerializeField] GameObject triggerText;
    [SerializeField] TextMesh triggerTextMesh;
    [SerializeField] GameObject VFX_healthStation;
    [SerializeField] GameObject VFX_HealingPrefab;
    [SerializeField] float healingValue = 100;
    [SerializeField] bool isTriggered = false;

    [SerializeField] PlayerControl playerControl;

    void Start () {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        triggerText.SetActive(false);
        triggerTextMesh = triggerText.GetComponent<TextMesh>();

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
            healthpack_sfx.Play();
            playerControl.heroStatusPanelControl.RefreshHPBarDisplay();
            GameObject temp_vfx = Instantiate(VFX_HealingPrefab, transform.position, Quaternion.identity);
            Destroy(temp_vfx, 2.0f);
            VFX_healthStation.SetActive(false);
        }
        triggerText.SetActive(true);
        triggerTextMesh.color = new Color(255, 255, 255, 0);
        StartCoroutine("DisplayText");
    }

    IEnumerator DisplayText()
    {
        while (triggerTextMesh.color.a < 1)
        {
            triggerTextMesh.color = new Color(triggerTextMesh.color.r, triggerTextMesh.color.g, triggerTextMesh.color.b, triggerTextMesh.color.a + 0.01f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(1.0f);
        while (triggerTextMesh.color.a > 0)
        {
            triggerTextMesh.color = new Color(triggerTextMesh.color.r, triggerTextMesh.color.g, triggerTextMesh.color.b, triggerTextMesh.color.a - 0.01f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
