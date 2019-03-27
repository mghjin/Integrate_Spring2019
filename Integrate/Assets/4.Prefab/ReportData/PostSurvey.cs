using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostSurvey : MonoBehaviour
{
    public string base_URL = "https://docs.google.com/forms/d/e/1FAIpQLScycmoVCvu3Ea9UYr75_o9DSxcmvuxLv2tFdZBphaKfaEqPJg/formResponse";
    WWWForm form;
    public GameObject[] questionSheets;
    public string[] answers;
    public int currentSheetIndex = 0;

    private void Start()
    {
        for (int i = 0; i <= 12; i++)
        {
            questionSheets[i].SetActive(false);
        }
        questionSheets[0].SetActive(true);
        currentSheetIndex = 0;
    }

    public void DisplayNextSheet()
    {
        questionSheets[currentSheetIndex].SetActive(false);
        currentSheetIndex++;
        questionSheets[currentSheetIndex].SetActive(true);

        if (currentSheetIndex == 12)
        {
            StartCoroutine(SendToForm());
        }
    }

    IEnumerator SendToForm()
    {

        WWWForm form = new WWWForm();
        form.AddField("entry.1133445076", answers[1]);
        form.AddField("entry.764647338", answers[2]);
        form.AddField("entry.1315886282", answers[3]);
        form.AddField("entry.877807927", answers[4]);
        form.AddField("entry.44599190", answers[5]);
        form.AddField("entry.816288808", answers[6]);
        form.AddField("entry.11216890", answers[7]);
        form.AddField("entry.665484685", answers[8]);
        form.AddField("entry.1545406700", answers[9]);
        form.AddField("entry.1996678731", answers[10]);
        form.AddField("entry.1868458349", answers[11]);
        form.AddField("entry.614804790", answers[12]);

        byte[] rawData = form.data;
        WWW www = new WWW(base_URL, rawData);
        yield return www;

        Debug.Log("Data sent!");

        Debug.Log(form.data);
        Debug.Log(www);
    }

    public void SendSelectedAnswerToFormat(int a)
    {
        int answerNumber;
        int answerScale;

        answerNumber = a / 10;
        answerScale = a % 10;
        answers[answerNumber] = answerScale.ToString();
    }


}
