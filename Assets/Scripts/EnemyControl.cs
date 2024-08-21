using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    Rigidbody2D enemyRb;
    public float speed;
    public float enemyHealth = 20;
    private GameObject player;
    private PlayerMovement PlayerMovement;
    private float powerupStrength = 2;

    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        enemyHealth = enemyHealth * PlayerMovement.levelDifficulty;
    }

    void Update()
    {
        Vector2 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (enemyHealth < 0)
        {
            Destroy(gameObject);
            PlayerMovement.PlayerSkillPoints += 10;

        }

        if (PlayerMovement.AcentIsActive == false)
        {
            Destroy(gameObject);
        }

        if (transform.position.y < -70)
        {
            Destroy(gameObject);
        }
    }


    void Damage(int damage)
    {
        //This is the code minuses damage from the player's health. 
        enemyHealth -= damage/ PlayerMovement.levelDifficulty;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            // Gets destroyed if it touches the player's weapon.
            Damage(20);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // KnockBack code. 
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 direction = (transform.position - collision.gameObject.transform.position).normalized;
            enemyRb.AddForce(direction * powerupStrength, ForceMode2D.Impulse);
        }
    }
}
