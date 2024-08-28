using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    // Establishes that the enemies health is 100.
    public float enemyHealth = 100;
    public bool BossisDead;
    private PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        // Finds player so that this script can get infomation from the PlayerMovement script.
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy's health is below 0, then they arte bought to the death function and destroyed.
        if (enemyHealth < 0)
        {
            Death();

        }
    }
    public void Damage(int damage)
    {
        //This is the code minuses damage from the player's health. 
        enemyHealth -= damage / Player.levelDifficulty * Player.AttackPower;
    }

    private void Death()
    {
        // Annouces that the bossisdead so that the program can act accordingly to act on the bosses death.
        BossisDead = true;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            // Takes Damage when in contact with the Player's weapon.
            Damage(20);

        }
    }
}
