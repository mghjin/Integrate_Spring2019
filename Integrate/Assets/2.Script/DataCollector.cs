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

public class DataCollector : MonoBehaviour
{
    // collects int values for viruses per level,
    // viruses killed per level,
    // health stations (lore) triggered per level,
    // player's rebel level throughout levels
    public int[] data_AmountOfVirus_Level = new int[5];
    public int[] data_AmountOfKilling_Level = new int[5];
    public int[] data_AmountOfTriggeredHealthStation = new int[5];
    public int[] data_RebelliousLevel_Level = new int[5];

}
