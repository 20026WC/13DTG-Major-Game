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

    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            // Gets destroyed if it touches the player's weapon.
            enemyHealth -= 10;

        }
    }
}
