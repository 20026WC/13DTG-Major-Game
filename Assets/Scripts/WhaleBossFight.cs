using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleBossFight : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private GameObject Spawn;
    public int waveNumber = 1;
    public int enemyCount;
    public float EnemyHealth = 100;
    public GameObject EndLevelDoor;

    public bool WhaleisDead;
    public GameObject Enemy;

    private PlayerMovement PlayerMovement;
    private EattingEnemy Whale;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Whale = GameObject.Find("Giant Enemy").GetComponent<EattingEnemy>();
        WhaleisDead = false;


    }

    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyControl>().Length;
        if (Whale.Stop == true)
        {
            if (enemyCount == 0)
            {
                int ran = UnityEngine.Random.Range(1, 3);
                waveNumber++; SpawnEnemyWave(ran);
                EnemyHealth -= 20;
            }

            if (EnemyHealth <= 0)
            {
                EndLevelDoor.SetActive(true);
                WhaleisDead = true;
                Destroy(Enemy.gameObject);
            }
        }


    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Spawn = GameObject.Find("WhaleEnemySpawner");
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], Spawn.transform.position, enemyPrefabs[enemyIndex].transform.rotation);
        }
    }
}