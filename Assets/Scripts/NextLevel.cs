using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private PlayerMovement Player;
    private RandomisedScript RandomisedScript;
    public bool StartNextLevel;
    public GameObject TheBossLevel;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        RandomisedScript = GameObject.Find("Levels").GetComponent<RandomisedScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.AcentIsActive == false)
        {
            AfterMathOfbATTLE();
        }


        if (Player.StartNewLevel == true)
        {
            AfterMathOfbATTLE();
            StartNextLevel = true;
            Player.StartNewLevel = false;
            RandomisedScript.DefeatedBosses += 1;

        }
    }

    public void AfterMathOfbATTLE()
    {
        RandomisedScript.BeginAboss = false;
        Destroy(TheBossLevel.gameObject);
    }
}
