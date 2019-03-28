using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PostSurvey : MonoBehaviour
{
    public string base_URL = "https://docs.google.com/forms/d/e/1FAIpQLScycmoVCvu3Ea9UYr75_o9DSxcmvuxLv2tFdZBphaKfaEqPJg/formResponse";
    WWWForm form;
    public GameObject[] questionSheets;
    public string[] answers;
    public int currentSheetIndex = 0;

    DataCollector dataCollector;

    private void Start()
    {
        dataCollector = FindObjectOfType<DataCollector>();
        for (int i = 0; i <= 12; i++)
        {
            questionSheets[i].SetActive(false);
        }
        questionSheets[0].SetActive(true);
        currentSheetIndex = 0;


        //get data from dataCollector


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
        //answers
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

        //data
        form.AddField("entry.286344326", dataCollector.data_AmountOfVirus_Level[0]);
        form.AddField("entry.1883973640", dataCollector.data_AmountOfVirus_Level[1]);
        form.AddField("entry.927227775", dataCollector.data_AmountOfVirus_Level[2]);
        form.AddField("entry.387211805", dataCollector.data_AmountOfVirus_Level[3]);
        form.AddField("entry.833904805", dataCollector.data_AmountOfVirus_Level[4]);

        form.AddField("entry.1409470758", dataCollector.data_AmountOfKilling_Level[0]);
        form.AddField("entry.638780155", dataCollector.data_AmountOfKilling_Level[1]);
        form.AddField("entry.929450014", dataCollector.data_AmountOfKilling_Level[2]);
        form.AddField("entry.1120369409", dataCollector.data_AmountOfKilling_Level[3]);
        form.AddField("entry.529513921", dataCollector.data_AmountOfKilling_Level[4]);

        form.AddField("entry.1019745512", dataCollector.data_AmountOfTriggeredHealthStation[0]);
        form.AddField("entry.2099927699", dataCollector.data_AmountOfTriggeredHealthStation[1]);
        form.AddField("entry.1923076245", dataCollector.data_AmountOfTriggeredHealthStation[2]);
        form.AddField("entry.1727733272", dataCollector.data_AmountOfTriggeredHealthStation[3]);
        form.AddField("entry.699606845", dataCollector.data_AmountOfTriggeredHealthStation[4]);

        form.AddField("entry.198557040", dataCollector.data_RebelliousLevel_Level[0]);
        form.AddField("entry.820824247", dataCollector.data_RebelliousLevel_Level[1]);
        form.AddField("entry.58726270", dataCollector.data_RebelliousLevel_Level[2]);
        form.AddField("entry.1375999688", dataCollector.data_RebelliousLevel_Level[3]);
        form.AddField("entry.1772165519", dataCollector.data_RebelliousLevel_Level[4]);


        byte[] rawData = form.data;
        WWW www = new WWW(base_URL, rawData);
        yield return www;

        /*UnityWebRequest www = UnityWebRequest.Post(base_URL, form);
        yield return www.SendWebRequest(); 

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Data sent!");
        }*/

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
