using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingShop : MonoBehaviour
{
    // This assigns skill; tree to an object that this script cna make visble and invisble when it needs to be
    public GameObject SkillTree;
    private PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        // Finds trhe player so that this script can acess PlayerMovment to see if shooping is set to true. 
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // This makes the skill tree visible if the shopping varible in Player is set to true.
        if (PlayerMovement.Shopping == true)
        {
            SkillTree.SetActive(true);
        }
        else
        // If playerMovment doesn't have shopping set to true then the skill tree will not be visable. 
        { SkillTree.SetActive(false); }
    }
}
