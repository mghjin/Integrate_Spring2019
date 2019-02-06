using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExitControl : MonoBehaviour
{
    LevelManager levelManager;
    public float[] percentLevels; //if killing rate <= percentLevels[n], then display levelClearInfo[n]
    [TextArea(15, 20)]
    public string[] levelClearInfo;
    [SerializeField] Image levelClearPanel;
    [SerializeField] Text levelClearText;
    [SerializeField] float killingRate = 0; // 0 - 100

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelClearText = GameObject.Find("LevelClearInfo").GetComponent<Text>();
        levelClearPanel = GameObject.Find("LevelClearPanel").GetComponent<Image>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "player")
        {
            PlayerControl playerControl = other.GetComponent<PlayerControl>();
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            playerControl.canControl = false;
            playerControl.isInvincible = true;
            rigidbody.velocity = Vector3.zero;
            rigidbody.isKinematic = true;
            killingRate = levelManager.eliminatingRate;

            for (int i=0; i <= percentLevels.Length - 2; i++)
            {
                if (killingRate >= percentLevels[i] && killingRate < percentLevels[i + 1])
                {
                    levelClearText.text = levelClearInfo[i];
                }
            }

            StartCoroutine("ProcessLevelClear");

        }
    }

    IEnumerator ProcessLevelClear()
    {
        float goalAlpha = 1;
        while (levelClearPanel.color.a < goalAlpha)
        {
            levelClearPanel.color = new Color(0, 0, 0, levelClearPanel.color.a + 0.01f);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        while (levelClearText.color.a < goalAlpha)
        {
            levelClearText.color = new Color(1, 1, 1, levelClearText.color.a + 0.01f);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return new WaitForSeconds(5);
        levelManager.LoadNextScene();
    }
}
