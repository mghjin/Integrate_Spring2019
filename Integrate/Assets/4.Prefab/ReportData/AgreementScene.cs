using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AgreementScene : MonoBehaviour
{
    [SerializeField] int currentSceneBuildIndex;

    public void LoadGame()
    {
        currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneBuildIndex++;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
