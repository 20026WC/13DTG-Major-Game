using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;
    private GameObject Spawn;

    public bool bossSpawned;
    public bool PlayerIsDead = false;
    public int waveNumber = 1;
    public int enemyCount;
    public int BossCount;

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
            BossCount = GameObject.FindGameObjectsWithTag("BossBattleLevel").Length;
            PlayerIsDead = false;
            if (enemyCount == 0)
            {
                if (RandomisedScript.BeginAboss == true)
                {
                    if (BossCount == 0)
                    {
                        SpawnEnemyWave(1, true);
                        PlayerMovement.Spawned = false;
                    }


                }
                else
                {
                    RandomisedScript.RandomNumber();
                    int ran = UnityEngine.Random.Range(1, 3);
                    waveNumber++; SpawnEnemyWave(ran, false);
                    PlayerMovement.Spawned = false;
                    RandomisedScript.DefeatedStage += 1;
                }


            }



        }

    }
    public void PlayerHasDied()
    {
        PlayerIsDead = true;
    }

    void SpawnEnemyWave(int enemiesToSpawn, bool IsBoss)
    {
        Spawn = GameObject.Find("EnemySpawn");
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex;

            if (IsBoss == true && !bossSpawned)
            {
                enemyIndex = UnityEngine.Random.Range(0, bossPrefabs.Length);
                Instantiate(bossPrefabs[enemyIndex], Spawn.transform.position, bossPrefabs[enemyIndex].transform.rotation);
                bossSpawned = true;
            }
            if (IsBoss == false && bossSpawned)
            {
                enemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[enemyIndex], Spawn.transform.position, enemyPrefabs[enemyIndex].transform.rotation);
            }
        }
    }
}
