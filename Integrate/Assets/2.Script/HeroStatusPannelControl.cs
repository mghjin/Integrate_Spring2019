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

public class HeroStatusPannelControl : MonoBehaviour {
    #region References
    [SerializeField] Image HeroHPBar;
    [SerializeField] Image HeroHPBarMaximum;
    PlayerControl playerControl;
    #endregion


    void Start () {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
	}
	
    public void RefreshHPBarDisplay()
    {
        HeroHPBar.gameObject.transform.localScale = new Vector3(playerControl.currentHP / 100f, 1, 1);
        HeroHPBarMaximum.gameObject.transform.localScale = new Vector3(playerControl.maxHP / 100f, 1, 1);
    }
}
