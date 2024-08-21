using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public float enemyHealth = 100;
    public bool BossisDead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth < 0)
        {
            Destroy(gameObject);
            BossisDead = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            // Gets destroyed if it touches the player's weapon.
            enemyHealth -= 10;

        }
    }
}
