using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Permissions;
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

    public GameObject Weapon;
    private GameObject Spawn;
    private RandomisedScript RandomisedScript;

    public bool Attacking;
    public bool GameIsActive;
    public bool Spawned;
    public bool lookingleft = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        RandomisedScript = GameObject.Find("Levels").GetComponent<RandomisedScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsActive)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            // Space.world ignores the object's rotation and keeps the player moving in the same direction.
            transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed, Space.World);


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

            // Code for the playere to jump when space is pressed. 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            }



            if ((RandomisedScript.TeleportPlayer = true) && !Spawned)
            {
                Spawned = true;
                PlayerSpawnPoint();
                
            }
        }

        // This kills the player if they reach a higght below -20.

        // Takes player out when heath gets to 0. 
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        if (transform.position.y < -70)
        {
            currentHealth -= maxHealth;
        }

    }

    void Damage(int damage)
    {
        //This is the code minuses damage from the player's health. 
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
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
    }

    public void startGame()
    {
        PlayerSpawnPoint();
        Physics.gravity *= gravityModifier;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        Spawned = false;
        gameObject.SetActive(true);
    }

    public void beginAcent()
    {
        GameIsActive = true;
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
