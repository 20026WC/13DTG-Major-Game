using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisedScript : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public bool TeleportPlayer;

    private PlayerMovement PlayerMovement;

    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        
    }


    public void RandomNumber()
    {
        int ran = UnityEngine.Random.Range(1, 4);
        TeleportPlayer = true;
        if (ran == 1)
        {
            level1.SetActive(true);
            level2.SetActive(false);
            level3.SetActive(false);
        }
        else if (ran == 2)
        {
            level1.SetActive(false);
            level2.SetActive(true);
            level3.SetActive(false);
        }
        else
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(true);
        }
        TeleportPlayer = false;
    }
}
