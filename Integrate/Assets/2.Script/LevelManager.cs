using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int numberOfEnemiesInThisScene = 0;
    public int numberOfEnemiesBeenEliminated = 0;
    public float eliminatingRate = 0f;
    public EnemyControl[] enemies;

    void Start()
    {
        enemies = FindObjectsOfType<EnemyControl>();
        foreach (EnemyControl ec in enemies)
        {
            numberOfEnemiesInThisScene++;
        }
        Debug.Log(numberOfEnemiesInThisScene);
    }

    public void calculateDeathOfEnemies()
    {
        numberOfEnemiesBeenEliminated++;
        eliminatingRate = (float)numberOfEnemiesBeenEliminated / (float)numberOfEnemiesInThisScene * 100;
        
    }

}
