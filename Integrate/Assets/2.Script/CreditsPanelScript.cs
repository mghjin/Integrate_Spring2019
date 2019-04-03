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
using UnityEngine.UI;

public class CreditsPanelScript : MonoBehaviour
{
    public GameObject CreditsPanel;
    bool status = false;
    public Button CreditsButton;

    private void Start()
    {
        CreditsButton.onClick.AddListener(ButtonClick);
    }

    public void ButtonClick()
    {
        status = !status;
        CreditsPanel.SetActive(status);
    }
}
