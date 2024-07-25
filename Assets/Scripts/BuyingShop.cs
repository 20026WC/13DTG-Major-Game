using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingShop : MonoBehaviour
{
    public GameObject Heath;
    public GameObject Attack;
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
            Attack.SetActive(true);
            Heath.SetActive(true);
        }
    }
}
