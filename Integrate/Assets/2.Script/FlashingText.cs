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

public class FlashingText : MonoBehaviour
{
    public Text LvTutorialText;

    private void Start()
    {
        LvTutorialText.gameObject.SetActive(true);
        //can set the float to any duration
        //text will appear on the screen for that amount of time
        Invoke("DisableText", 13f);
    }

    void DisableText()
    {
        LvTutorialText.gameObject.SetActive(false);
    }
}
