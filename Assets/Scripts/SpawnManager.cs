using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private GameObject Spawn;

    public int waveNumber = 1;
    public int enemyCount;

    private PlayerMovement PlayerMovement;
    private RandomisedScript RandomisedScript;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        RandomisedScript = GameObject.Find("Levels").GetComponent<RandomisedScript>();

    }

    void Update()
    {
        if (PlayerMovement.AcentIsActive == true)
        {
            enemyCount = FindObjectsOfType<EnemyControl>().Length;
            if (enemyCount == 0) 
            {
                RandomisedScript.RandomNumber();
                waveNumber++; SpawnEnemyWave(waveNumber);
                PlayerMovement.Spawned = false;

            }

            
        }
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Spawn = GameObject.Find("EnemySpawn");
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], Spawn.transform.position , enemyPrefabs[enemyIndex].transform.rotation);
        }
    }
}
