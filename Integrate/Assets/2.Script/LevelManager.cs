using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This manager won't be detroyed after loading
public class LevelManager : MonoBehaviour
{

    public int numberOfEnemiesInThisScene = 0;
    public int numberOfEnemiesBeenEliminated = 0;
    public int rebelliousLevel = 0; // this parameter indicates the times player goes against the order. It won't be initialized on loading.
    public float eliminatingRate = 0f;
    public EnemyControl[] enemies;
    [SerializeField] int currentSceneBuildIndex;


    void Awake()
    {
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
        SceneManager.LoadScene(currentSceneBuildIndex + 1);
    }

}
