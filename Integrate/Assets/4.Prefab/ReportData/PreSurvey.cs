using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreSurvey : MonoBehaviour
{
    [SerializeField] string base_URL = "https://docs.google.com/forms/d/e/1FAIpQLSeGbjyUWase4-kPxrQn-B5wvs1e5LOtTPnhKKjGt4L8wOXLhg/formResponse";
    [SerializeField] string answer;

    IEnumerator SendToForm(string answer)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1947089699", answer);
        byte[] rawData = form.data;

        WWW www = new WWW(base_URL, rawData);
        yield return www;
        //yield return new WaitForSecondsRealtime(1f);
    }

    public void Send()
    {
        StartCoroutine(SendToForm(answer));
    }

    public void SetAnswer(string selectedAnswerString)
    {
        answer = selectedAnswerString;
        Send();
    }
}
