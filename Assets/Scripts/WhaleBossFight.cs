using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleBossFight : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private GameObject Spawn;
    // establishes that wavenumber is 1 at the start of the game. 
    public int waveNumber = 1;
    // This counts the enemyCount
    public int enemyCount;
    // establishes that that the EnemyHealth is 100.
    public float EnemyHealth = 100;
    // This is the reference of the endlevel door.
    public GameObject EndLevelDoor;

    // establishes that the WhaleisDead is not dead at this point.
    public bool WhaleisDead = false;
    public GameObject Enemy;

    // Script refernces
    private PlayerMovement PlayerMovement;
    private EattingEnemy Whale;
    // Start is called before the first frame update
    void Start()
    {
        // Find the player mdoel and gets the PlayerMovement script
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        // Finds the EattingEnemy Script from the giant enemy game object. 
        Whale = GameObject.Find("Giant Enemy").GetComponent<EattingEnemy>();


    }

    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyControl>().Length;
        if (Whale.Stop)
        {
            // If the enemy count reaches 0 while the enemy is still alive then a new wave is spawned. 
            if (enemyCount == 0)
            {
                // waveNumber++ increases the wvae number by 1.
                waveNumber++; SpawnEnemyWave(waveNumber);
                // This gives the whale enemy a damage of 10. 
                EnemyHealth -= 10;
            }

            // If the enemy's health reaches 0 then it's modle will be deleted from the heirarchy. 
            if (EnemyHealth <= 0)
            {
                // This summons the end level door before the modle is deleted. 
                EndLevelDoor.SetActive(true);
                // This bool notifys all  scripts which need the whale ewnemy that the whale enemy is dead. 
                WhaleisDead = true;
                // 
                Destroy(Enemy.gameObject);
            }
        }


    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Summons enemies until the whale enemy loses all of it's health.
        Spawn = GameObject.Find("WhaleEnemySpawner");
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
            // This sends a prefab of the enemyIndex out of the closest spawner it can find
            Instantiate(enemyPrefabs[enemyIndex], Spawn.transform.position, enemyPrefabs[enemyIndex].transform.rotation);
        }
    }
}