using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Prefab list of enemyPrefabs.
    public GameObject[] enemyPrefabs;
    // Prefab list of bossPrefabs.
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
        if (PlayerMovement.AcentIsActive && !PlayerMovement.BeginFinal)
        {
            // this counts all object with the enemy tag into enemy count
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            // this does the same, but does it with bosses with the tag BossBattleLevel.
            BossCount = GameObject.FindGameObjectsWithTag("BossBattleLevel").Length;
            PlayerIsDead = false;
            if (enemyCount == 0)
            {
                if (RandomisedScript.BeginAboss == true)
                {
                    if (BossCount == 0)
                    {
                        // This sommons a singular boss
                        SpawnEnemyWave(1, true);
                        PlayerMovement.Spawned = false;
                    }


                }
                else
                {
                    RandomisedScript.RandomNumber();
                    // this summons how ever many enemies were randomly chosen by ran.
                    int ran = UnityEngine.Random.Range(1, 3);
                    waveNumber++; SpawnEnemyWave(ran, false);
                    PlayerMovement.Spawned = false;
                    RandomisedScript.DefeatedStage += 1;
                }


            }
        }

    }
    // Indicates that the player is dead. this tells all of the scripts that have acess to this script that the player is dead. 
    public void PlayerHasDied()
    {
        PlayerIsDead = true;
    }

    // bool is boss spawns a boss when true and a normla enemy when false. 
    void SpawnEnemyWave(int enemiesToSpawn, bool IsBoss)
    {
        // this finds the nearest active spawnpoint.
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
                // Spawns the enemy at the spawn points location.
                Instantiate(enemyPrefabs[enemyIndex], Spawn.transform.position, enemyPrefabs[enemyIndex].transform.rotation);
            }
        }
    }
}
