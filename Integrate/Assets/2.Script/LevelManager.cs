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
using UnityEngine.SceneManagement;


//This manager won't be detroyed after loading
public class LevelManager : MonoBehaviour
{

    public int numberOfEnemiesInThisScene = 0;
    public int numberOfEnemiesBeenEliminated = 0;
    public int numberOfEnemies_SummedUp = 0;                      //add numberOfEnemiesInThisScene to this variable to calculate the total number
    public int numberOfEnemiesBeenEliminated_SummedUp = 0;        //add numberOfEnemiesBeenEliminated to this variable to calculate the total number
    public int rebelliousLevel = 0; // this parameter indicates the times player goes against the order. It won't be initialized on loading.
    public float eliminatingRate = 0f;
    public EnemyControl[] enemies;
    [SerializeField] int currentSceneBuildIndex = 0;


    void Awake()
    {
        currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("LevelManager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //calculating number of enemies
        enemies = FindObjectsOfType<EnemyControl>();
        foreach (EnemyControl ec in enemies)
        {
            numberOfEnemiesInThisScene++;
        }
        Debug.Log(numberOfEnemiesInThisScene);


        //scene management
        currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

    }

    public void calculateDeathOfEnemies()
    {
        numberOfEnemiesBeenEliminated++;
        eliminatingRate = (float)numberOfEnemiesBeenEliminated / (float)numberOfEnemiesInThisScene * 100;
    }

    public void LoadNextScene()
    {


        if (eliminatingRate < 100)
        {
            rebelliousLevel++;
        }
        Debug.Log("A:Now the currentSceneBuildIndex is:" + currentSceneBuildIndex);
        currentSceneBuildIndex++;
        Debug.Log("B:Now the currentSceneBuildIndex is:" + currentSceneBuildIndex);
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

    public void LevelEndCalculate()
    {
        numberOfEnemies_SummedUp += numberOfEnemiesInThisScene;                     //add the quantity of enemies in current level to the sum
        numberOfEnemiesBeenEliminated_SummedUp += numberOfEnemiesBeenEliminated;    //add the quantity of eliminated enemies in current level to the sum
    }

}
