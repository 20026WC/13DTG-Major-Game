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

    private PlayerMovement PlayerMovement;

    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        
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
        if (!BeginAboss)
        {
            int ran = UnityEngine.Random.Range(1, 6);
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
            else if (ran == 4)
            {
                level1.SetActive(false);
                level2.SetActive(false);
                level3.SetActive(false);
                level4.SetActive(true);
                BeginAboss = true;

            }
            TeleportPlayer = false;
        }

    }
}
