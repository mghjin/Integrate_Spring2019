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

public class CreditsPanelScript : MonoBehaviour
{
    public GameObject CreditsPanel;
    bool status = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            status = !status;
            CreditsPanel.SetActive(status);
        }
    }
}
