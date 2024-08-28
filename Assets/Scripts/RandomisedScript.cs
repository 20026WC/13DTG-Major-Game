using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisedScript : MonoBehaviour
{
    public GameObject levels;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public bool TeleportPlayer;
    public bool BeginAboss;

    public int DefeatedStage = 0;
    public int DefeatedBosses = 0;
    private SkillTree SP;

    private PlayerMovement PlayerMovement;
    private SpawnManager SM;

    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        SP = GameObject.Find("Skill Tree").GetComponent<SkillTree>();
        SM.bossSpawned = true;

    }

    public void StartofGame()
    {
        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
        level4.SetActive(false);
    }

    public void RandomNumber()
    {
        if (!BeginAboss && !PlayerMovement.BeginFinal)
        {
            int ran = UnityEngine.Random.Range(1, 4);
            TeleportPlayer = true;
            if (ran == 1)
            {
                level1.SetActive(true);
                level2.SetActive(false);
                level3.SetActive(false);
                level4.SetActive(false);
            }
            else if (ran == 2)
            {
                level1.SetActive(false);
                level2.SetActive(true);
                level3.SetActive(false);
                level4.SetActive(false);
            }
            else if (ran == 3)
            {
                level1.SetActive(false);
                level2.SetActive(false);
                level3.SetActive(true);
                level4.SetActive(false);
            }

            if (DefeatedStage >= 5)
            {
                SM.bossSpawned = false;
                level1.SetActive(false);
                level2.SetActive(false);
                level3.SetActive(false);
                level4.SetActive(true);
                BeginAboss = true;
                DefeatedStage = 0;
            }

            TeleportPlayer = false;
            SP.SkillPoints += 1;
        }

        if (PlayerMovement.BeginFinal)
        {
            TeleportPlayer = true;
            SM.bossSpawned = false;
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            level4.SetActive(false);
            TeleportPlayer = false;
        }

    }

}
