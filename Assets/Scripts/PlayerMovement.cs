using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRb;

    public float jumpForce;
    public float horizontalInput;
    public float speed = 20.0f;
    public float gravityModifier = 2.0f;
    private float powerupStrength = 200000000;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    
    public GameObject basespawn;
    public GameObject Weapon;
    public GameObject GameOver;
    private GameObject Spawn;
    private RandomisedScript RandomisedScript;

    public bool Attacking;
    public bool GameIsActive;
    public bool AcentIsActive;
    public bool Spawned;
    public bool Shopping;
    public bool lookingleft = true;
    public bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        RandomisedScript = GameObject.Find("Levels").GetComponent<RandomisedScript>();
        GameIsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsActive)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            // Space.world ignores the object's rotation and keeps the player moving in the same direction.
            transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed, Space.World);

            // Code for the playere to jump when space is pressed. 
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isOnGround = false;

            } 
            // If the player is not looking left and pushes A they are rotated to the left.
            if (Input.GetKeyDown(KeyCode.A) && !lookingleft)
            {
                transform.Rotate(0f, 180f, 0f);
                lookingleft = true;
            }

            // If the player is looking left and pushes D they are rotated to the right.
            if (Input.GetKeyDown(KeyCode.D) && lookingleft)
            {
                transform.Rotate(0f, -180f, 0f);
                lookingleft = false;
            }


            // Summons sword when player presses the left mouse key. 
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Starts countdown for when to despawn sword
                StartCoroutine(PlayerAttackCountdownRoutine());
            }


            // This kills the player if they reach a higght below -20.

            // Takes player out when heath gets to 0. 
            if (currentHealth <= 0)
            {
                Death();
            }
            else
            {
                GameOver.SetActive(false);
            }

            if (transform.position.y < -70)
            {
                Death();
            }


            if ((RandomisedScript.TeleportPlayer = true) && !Spawned)
            {
                Spawned = true;
                PlayerSpawnPoint();
                
            }
        }


    }

    void Damage(int damage)
    {
        //This is the code minuses damage from the player's health. 
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }


    public void Death()
    {
        currentHealth -= maxHealth;
        gameObject.SetActive(false);
        GameOver.SetActive(true);
    }
    public void startGame()
    {
        PlayerSpawnPoint();
        Physics.gravity *= gravityModifier;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        Spawned = false;
        AcentIsActive = false;
        gameObject.SetActive(true);
        basespawn.SetActive(true);

    }



    void PlayerSpawnPoint()
    {
        Spawn = GameObject.Find("SpawnPoint");
        gameObject.transform.position = Spawn.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !Attacking)
        {
            // Tells the program to take 1o from player's health.
            Damage(10);         
        }

        if (other.gameObject.CompareTag("AcentBeginner"))
        {
            startGame();
            AcentIsActive = true;
            basespawn.SetActive(false);
        }

        if (other.gameObject.CompareTag("Shop"))
        {
            Shopping = true;
        }
        else
        {
            Shopping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 direction = (transform.position - collision.gameObject.transform.position).normalized;
            enemyRb.AddForce(direction * powerupStrength, ForceMode2D.Impulse);
        }
    }

    IEnumerator PlayerAttackCountdownRoutine()
    {
        Weapon.SetActive(true);
        Attacking = true;
        // Allows sowrd to be active for a second.
        yield return new WaitForSeconds(1);
        Weapon.SetActive(false);
        Attacking = false;
    }
}
