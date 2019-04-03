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
using UnityEngine.SceneManagement;


//This manager won't be detroyed after loading
public class LevelManager : MonoBehaviour
{

    public int[] data_killingRate;

    public int numberOfEnemiesInThisScene = 0;
    public int numberOfEnemiesBeenEliminated = 0;
    public int numberOfEnemies_SummedUp = 0;                      //add numberOfEnemiesInThisScene to this variable to calculate the total number
    public int numberOfEnemiesBeenEliminated_SummedUp = 0;        //add numberOfEnemiesBeenEliminated to this variable to calculate the total number
    public int rebelliousLevel = 0;                               // this parameter indicates the times player goes against the order. It won't be initialized on loading.
    public float eliminatingRate = 0f;
    public int amountOfTriggeredHealthStation = 0;              //for data collecting
    public EnemyControl[] enemies;
    [SerializeField] int currentSceneBuildIndex = 0;

    [SerializeField] DataCollector dataCollector;          //drag for reference


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
        //initialization
        numberOfEnemiesInThisScene = 0;
        numberOfEnemiesBeenEliminated = 0;
        eliminatingRate = 0f;
        amountOfTriggeredHealthStation = 0;

        //calculating number of enemies
        enemies = FindObjectsOfType<EnemyControl>();
        foreach (EnemyControl ec in enemies)
        {
            if (ec.dontCountWhenCalculatingTotalNumberOfEnemies == false)
            {
                numberOfEnemiesInThisScene++;
            }

        }
        Debug.Log(numberOfEnemiesInThisScene);



        //scene management
        currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

    }

    // for data tracking through levels
    public void calculateDeathOfEnemies()
    {
        numberOfEnemiesBeenEliminated++;
        eliminatingRate = (float)numberOfEnemiesBeenEliminated / (float)numberOfEnemiesInThisScene * 100;
    }

    public void LoadNextScene()
    {
        //deal data collecting
        dataCollector.data_AmountOfKilling_Level[currentSceneBuildIndex - 2] = numberOfEnemiesBeenEliminated;
        dataCollector.data_AmountOfVirus_Level[currentSceneBuildIndex - 2] = numberOfEnemiesInThisScene;
        dataCollector.data_RebelliousLevel_Level[currentSceneBuildIndex - 2] = rebelliousLevel;
        dataCollector.data_AmountOfTriggeredHealthStation[currentSceneBuildIndex - 2] = amountOfTriggeredHealthStation;


        //load scene
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
