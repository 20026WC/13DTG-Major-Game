using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRb;
    private IncreaseDiff sliderValue;

    public float jumpForce;
    public float horizontalInput;
    public float speed = 20.0f;
    public float gravityModifier = 2.0f;
    private float powerupStrength = 2;

    public int maxHealth;
    public int AttackPower = 1;
    public int currentHealth;
    public int PlayerSkillPoints;
    public int levelDifficulty;

    public int Heathupgraded;
    public int Attackupgraded;

    public HealthBar healthBar;
    public GameObject basespawn;
    public GameObject Weapon;
    public GameObject AirAttack;
    public GameObject GameOver;
    public GameObject Fkey;
    public GameObject TitleScreen;
    
    public Button NewAdventureButton;

    private GameObject Spawn;
    private RandomisedScript RandomisedScript;

    public bool Attacking;
    public bool GameIsActive;
    public bool AcentIsActive;
    public bool Spawned;
    public bool Shopping;
    public bool PlayerPaused;
    public bool beginShopping;
    public bool SelectDiff;
    public bool lookingleft = true;
    public bool isOnGround = true;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        sliderValue = GameObject.Find("Slider").GetComponent<IncreaseDiff>();
        RandomisedScript = GameObject.Find("Levels").GetComponent<RandomisedScript>();
        TitleScreen.SetActive(true);  
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsActive)
        {
            if (!PlayerPaused) 
            {
                horizontalInput = Input.GetAxis("Horizontal");
                // Space.world ignores the object's rotation and keeps the player moving in the same direction.
                transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed, Space.World);
            }

            levelDifficulty = (int)sliderValue.diffculty;
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

            if (Input.GetKeyDown(KeyCode.F))
            {
                ShoppingforUpgrades();
            }

            // Summons sword when player presses the left mouse key. 
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Starts countdown for when to despawn sword
                StartCoroutine(PlayerAttackCountdownRoutine());

                if (!isOnGround)
                {
                    playerRb.AddForce(Vector2.down * jumpForce * 2, ForceMode2D.Impulse);                
                    StartCoroutine(PlayerAirAttackCountdownRoutine());

                }
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
                NewAdventureButton.gameObject.SetActive(false);
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
        currentHealth -= damage * levelDifficulty;
        healthBar.SetHealth(currentHealth);
    }


    public void Death()
    {
        
        currentHealth -= maxHealth;
        gameObject.SetActive(false);
        GameOver.SetActive(true);
        NewAdventureButton.gameObject.SetActive(true);

    }
    public void startGame()
    {
        PlayerSpawnPoint();
        Physics.gravity *= gravityModifier;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        GameIsActive = true;
        PlayerPaused = false;
        Spawned = false;
        AcentIsActive = false;
        gameObject.SetActive(true);
        basespawn.SetActive(true);
        TitleScreen.gameObject.SetActive(false);

    }

    public void ShoppingforUpgrades()
    {
        if (beginShopping && Shopping) 
        { 
            Shopping = false;
            PlayerPaused = false;
            
        }
        else if (beginShopping && !Shopping)
        {
            Shopping = true;
            PlayerPaused = true;
        }
    }

    public void increaseHeath()
    {
        // Only upgraded heath if player has upgraded heath less then 10 times. 
        if (Heathupgraded <= 9)
        {
            maxHealth += 10;
            // Sends the new max health to the health bar slider. 
            healthBar.SetMaxHealth(maxHealth);
            Heathupgraded += 1;
        }

    }      
    
    public void increaseAttack()
    {
        // Only upgraded heath if player has upgraded heath less then 10 times. 
        if (Attackupgraded <= 9)
        {
            AttackPower += 10;
            Attackupgraded += 1;
        }

    }   
    

    void PlayerSpawnPoint()
    {
        // This finds the nearest game object with the tag SpawnPoint
        Spawn = GameObject.Find("SpawnPoint");
        // This sends the player gameobject over to the objecxt tagged with SpawnPoint
        gameObject.transform.position = Spawn.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !Attacking)
        {
            // Tells the program to that Enemys deal 10 damage from player's health.
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
            // Tells Script that the Player has begun shopping.
            Fkey.SetActive(true);
            beginShopping = true;
        }        
        if (other.gameObject.CompareTag("Diff"))
        {
            SelectDiff = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Shop"))
        {
            beginShopping = false;
            Fkey.SetActive(false);

        }
        if (other.gameObject.CompareTag("Diff"))
        {
            SelectDiff = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // KnockBack code. 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 direction = (transform.position - collision.gameObject.transform.position).normalized;
            enemyRb.AddForce(direction * powerupStrength, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Jumpable"))
        {
            isOnGround = true;
        }
    }

    IEnumerator PlayerAttackCountdownRoutine()
    {
        Weapon.SetActive(true);
        Attacking = true;
        // Allows sowrd to be active for a second.
        yield return new WaitForSeconds(3);
        Weapon.SetActive(false);
        Attacking = false;
    }   
    
    IEnumerator PlayerAirAttackCountdownRoutine()
    {
        AirAttack.SetActive(true);
        Attacking = true;
        yield return new WaitForSeconds(1);
        Attacking = false;
        AirAttack.SetActive(false);
    }

}
