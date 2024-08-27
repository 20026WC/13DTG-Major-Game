using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public float EnemyHealth = 50;


    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        if (transform.position.x > 20)
        {
            transform.Rotate(0f, -180f, 0f);
            transform.position = new Vector2(transform.position.x, -10);
        }

        if (transform.position.x < 5)
        {
            transform.Rotate(0f, -180f, 0f);
            transform.position = new Vector3(transform.position.x, -10);
        }

        if (EnemyHealth > 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player hits the enemy's weakness it begins the enemies WeaknessHit animation. 
        if (other.gameObject.CompareTag("Weapon"))
        {
            // This disables the enemies NotHitState. The enemies NoHitSpace is the enemies' triggers which damage the Player.
            EnemyHealth -= 10;

        }

    }
}
