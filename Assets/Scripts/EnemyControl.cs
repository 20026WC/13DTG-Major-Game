using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    Rigidbody2D enemyRb;
    public float speed;
    private GameObject player;

    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector2 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            // Gets destroyed if it touches the player's weapon.
            Destroy(gameObject);

        }
    }
}
