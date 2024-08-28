using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class InfectedEnemies : MonoBehaviour
{
    Rigidbody2D enemyRb;
    public float speed = 50;
    public float enemyHealth = 5;
    private float powerupStrength = 20000000;


    private PlayerMovement PlayerMovement;
    private SpawnManager Spawner;


    public bool Knockback;
    public bool isRunning;
    public bool isFlipped = false;

    public Animator animator;
    private Transform Player;
    private SpriteRenderer rend;

    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player").transform;
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        animator = GetComponent<Animator>();



        Spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        rend = GetComponent<SpriteRenderer>();


        enemyHealth = enemyHealth * PlayerMovement.levelDifficulty;
    }

    void Update()
    {
        if ( isRunning == true)
        {
            Vector2 lookDirection = (Player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
            LookAtPlayer();
            animator.SetBool("Walking", true);
        }


        float dist = Vector2.Distance(Player.position, transform.position);

        if (dist < 2)
        {
            animator.SetBool("Walking", false);
            isRunning = false;
            animator.SetBool("PlayerAttacking ", true);
        }
        else
        {
            isRunning = true;
            animator.SetBool("PlayerAttacking ", false);
        }


        if (enemyHealth < 0)
        {
            Destroy(gameObject);

        }

        if (!PlayerMovement.AcentIsActive)
        {
            death();
        }

        if (Spawner.PlayerIsDead == true)
        {
            death();
        }



    }

    public void LookAtPlayer()
    {
        // Check if the enemy is on the left side of the player and should face right
        if (transform.position.x < Player.transform.position.x && !isFlipped)
        {
            Flip();
        }
        // Check if the enemy is on the right side of the player and should face left
        else if (transform.position.x > Player.transform.position.x && isFlipped)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Flip the character by rotating 180 degrees on the Y axis
        transform.Rotate(0f, 180f, 0f);
        isFlipped = !isFlipped;  // Toggle the flipped state
    }

    void Damage(int damage)
    {
        //This is the code minuses damage from the player's health. 
        enemyHealth -= damage / PlayerMovement.levelDifficulty * PlayerMovement.AttackPower;
    }

    public void death()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            // Gets destroyed if it touches the player's weapon.
            Damage(20);
            animator.SetBool("PlayerWashit", true);
            StartCoroutine(CountdownRoutine());

        }

        if (other.gameObject.CompareTag("Death"))
        {
            // Gets destroyed if it touches the player's weapon.
            Destroy(gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a GameObject tagged as "Weapon"
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Knockback = true;
            // Get the Rigidbody2D component of the enemy (not the weapon)
            Rigidbody2D enemyRb = GetComponent<Rigidbody2D>();

            // Calculate the direction from the weapon to the enemy
            Vector2 direction = (transform.position - collision.gameObject.transform.position).normalized;

            // Apply the knockback force to the enemy in the opposite direction of the weapon
            enemyRb.AddForce(direction * powerupStrength, ForceMode2D.Impulse);
        }
    }

    IEnumerator CountdownRoutine()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("PlayerWashit", false);
    }
}
