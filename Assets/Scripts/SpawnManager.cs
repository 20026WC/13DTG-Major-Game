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
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Spawn = GameObject.Find("EnemySpawn");
        gameObject.transform.position = Spawn.transform.position;
    }

    void Update()
    {

        if (PlayerMovement.GameIsActive == true)
        {
            enemyCount = FindObjectsOfType<EnemyControl>().Length;
            if (enemyCount == 0) { waveNumber++; SpawnEnemyWave(waveNumber);}
        }
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], new Vector2(0, 6), enemyPrefabs[enemyIndex].transform.rotation);
        }
    }

    public void StartOfGame()
    {
        SpawnEnemyWave(waveNumber);
    }
}
