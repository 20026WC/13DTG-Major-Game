using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisedScript : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    void Start()
    {
        RandomNumber();
    }

    public void RandomNumber()
    {
        int ran = UnityEngine.Random.Range(1, 4);

        if (ran == 1)
        {
            level1.SetActive(true);
        }
        else if (ran == 2)
        {
            level2.SetActive(true);
        }
        else
        {
            level3.SetActive(true);
        }
    }
}
