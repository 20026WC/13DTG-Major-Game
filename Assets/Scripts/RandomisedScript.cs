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
        // Tells the spawnmanager script not to spawn anotehr boss by saying there is a boss spawned.
        SM.bossSpawned = true;

    }

    public void StartofGame()
    {
        // sets all the levels to false due to not wanting the player to spawn there rather than the base.
        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
        level4.SetActive(false);
    }

    // this code chsoes one of the 3 levels randomly if the player has not reached the final level.
    public void RandomNumber()
    {
        if (!BeginAboss && !PlayerMovement.BeginFinal)
        {
            // Choses a ranomly level from level 1 - level 3.
            int ran = UnityEngine.Random.Range(1, 4);
            // This indicates to teleport the player. 
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
                // Indicates to spawn a boss.
                BeginAboss = true;
                // sets the defetaed stages to 0 so the player cna play 5 more levels to encounter a second boss.
                DefeatedStage = 0;
            }
            // indicates that the player has already teleported to the next stage so not to do it again.
            TeleportPlayer = false;
            // Gains the player a singular skill point for all completed levels.
            SP.SkillPoints += 1;
        }
         // This sends the player to the final level when they have completed the requiremnts whioch is to complete 8-10 stages and 2 bosses.
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
