using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevel : MonoBehaviour
{
    public GameObject[] BossPrefabs;
    private GameObject Spawn;
    public int waveNumber = 1;
    public int BossCount;


    private PlayerMovement PlayerMovement;
    private Dialouge dialouge;
    private FinalBoss FB;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        dialouge = GameObject.Find("Dialouge Manager").GetComponent<Dialouge>();


    }

    void Update()
    {
        BossCount = FindObjectsOfType<FinalBoss>().Length;
        if (dialouge.BeginFinalBossFight == true)
        {
            if (BossCount == 0)
            {
                waveNumber++; SpawnEnemyWave(1);
            }

        }


    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Spawn = GameObject.Find("FinalBossSpawn");
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = UnityEngine.Random.Range(0, BossPrefabs.Length);
            Instantiate(BossPrefabs[enemyIndex], Spawn.transform.position, BossPrefabs[enemyIndex].transform.rotation);
        }
    }
}
