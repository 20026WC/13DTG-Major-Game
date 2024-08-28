using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    Rigidbody2D enemyRb;
    public float speed;
    // The enemies health is 20;
    public float enemyHealth = 20;
    // This references the player game object so that the scritp can find it.
    private GameObject player;
    public GameObject Cube;
    private PlayerMovement PlayerMovement;
    private SpawnManager Spawner;

    private float powerupStrength = 200;
    public bool Knockback;
    

    public Color HitColor;
    public Color NormalColor;
    private SpriteRenderer rend;

    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
        // This finds the player and informs the enemy where he is so that the player and enemy can fight.
        player = GameObject.Find("Player");
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        

        Spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        // Gets the SpriteRender component. This is how the Enemies clour is changed when they get hit. 
        rend = GetComponent<SpriteRenderer>();
        
        // This increases the enemies health if you play on harder difficulties.
        enemyHealth = enemyHealth * PlayerMovement.levelDifficulty;
    }

    void Update()
    {
        // This gets the distance between the player and the enemies possitions
        Vector2 lookDirection = (player.transform.position - transform.position).normalized;
        // This then adds speed so that the enemy can trvael that distance to get closier to the player. 
        enemyRb.AddForce(lookDirection * speed);

        // If the enemies Health reaches 0 they get destroyed.
        if (enemyHealth < 0)
        {
            Destroy(gameObject);

        }

        // These kill the enemy if the player is killed and reaches the base level.
        if (!PlayerMovement.AcentIsActive)
        {
            death();
        }

        if (Spawner.PlayerIsDead == true)
        {
            // Calls the death function
            death();
        }


        
    }


    void Damage(int damage)
    {
        //This is the code minuses damage from the player's health. 
        // This is the code which increases the damage the player exherts onto the enemy depednign on what atatck power they are at or the level difficulty. 
        enemyHealth -= damage / PlayerMovement.levelDifficulty * PlayerMovement.AttackPower;
    }

    // A function for destroying the game object. The enemy health one for some reason doesn't work with the function.
    public void death()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            // Takes a base damage of 20.
            Damage(20);
            // Starts the colour chnage of the enemy from red to black to indicate damage. 
            StartCoroutine(CountdownRoutine());

        }

        if (other.gameObject.CompareTag("Death"))
        {
            // Gets destroyed if it touches the death border.
            Destroy(gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a GameObject tagged as "Weapon"
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Knockback = true;
            // Get the Rigidbody2D component of the enemy
            Rigidbody2D enemyRb = GetComponent<Rigidbody2D>();

            // Calculate the direction from the weapon to the enemy
            Vector2 direction = (transform.position - collision.gameObject.transform.position).normalized;

            // Apply the knockback force to the enemy in the opposite direction of the weapon
            enemyRb.AddForce(direction * powerupStrength, ForceMode2D.Impulse);
        }
    }

    // This is when the enemy is hit by the player weapon. They turn red for a secodn before reverting to black.
    IEnumerator CountdownRoutine()
    {
        // Red
        rend.color = HitColor;
        // Waits 2 seconds before changing the enemy's colour from red to black.
        yield return new WaitForSeconds(2);
        // Black
        rend.color = NormalColor;
    }
}
