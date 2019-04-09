using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Display());

    }

    public void LoadingFinalSurvey()
    {
        SceneManager.LoadScene(7);
    }


    IEnumerator Display()
    {
        
        yield return new WaitForSeconds(180);
        animator.SetBool("IsDisplayed", true);
    }


}
