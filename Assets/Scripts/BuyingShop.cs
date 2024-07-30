using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingShop : MonoBehaviour
{
    public GameObject SkillTree;
    private PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.Shopping == true)
        {
            SkillTree.SetActive(true);
        }
        else
        { SkillTree.SetActive(false); }
    }
}
