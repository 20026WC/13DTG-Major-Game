using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public float enemyHealth = 100;
    public bool BossisDead;
    private PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
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
        BossisDead = true;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            // Gets destroyed if it touches the player's weapon.
            Damage(20);

        }
    }
}
