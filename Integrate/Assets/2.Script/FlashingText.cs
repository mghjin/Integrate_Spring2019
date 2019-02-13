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
using UnityEngine.UI;

public class FlashingText : MonoBehaviour
{

    public Text BossAIVoice;
    private AudioSource text_sfx;

    /* if we end up using this
     * the player will collide with a certain game object
     * in order for the text to trigger
     * can also play a certain sound effect
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            while (soundPlayed == false)
            {
                source.Play();
                soundPlayed = true;
            }
            FoundStrengthVirtue.gameObject.SetActive(true);
            Start();
        }
    }
    */

    //
    private void Start()
    {
        //can set the float to any duration
        //text will appear on the screen for that amount of time
        Invoke("DisableText", 10.5f);
    }

    void DisableText()
    {
        BossAIVoice.gameObject.SetActive(false);
    }
}
