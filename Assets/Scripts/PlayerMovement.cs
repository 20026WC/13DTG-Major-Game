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
    public int speed;
    public float gravityModifier = 2.0f;
    private float powerupStrength = 2;

    public int maxHealth;
    public int AttackPower = 1;
    public int currentHealth;
    public int PlayerSkillPoints;
    public int levelDifficulty;

    public int Heathupgraded;
    public int Attackupgraded;
    public int Speedupgraded;

    public HealthBar healthBar;
    public Animator animator;


    public GameObject TheHeathBar;
    public GameObject basespawn;
    public GameObject PlayersBaseSpawnPoint;
    public GameObject AirAttack;
    public GameObject GameOver;
    public GameObject Fkey;
    public GameObject TitleScreen;
    public GameObject TheFinalLevel;


    public Button NewAdventureButton;

    private GameObject Spawn;
    private RandomisedScript RandomisedScript;
    private SkillTree SP;


    public bool NODamage;
    public bool Leaving;
    public bool GameIsActive;
    public bool AcentIsActive;
    public bool Spawned;
    public bool Shopping;
    public bool PlayerPaused;
    public bool beginShopping;
    public bool SelectDiff;
    public bool lookingleft = true;
    public bool isOnGround = true;
    public bool StartNewLevel = false;
    public bool BeginFinal = false;
    public bool NearingEnding = false;
    public bool EndGame = false;




    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        sliderValue = GameObject.Find("Slider").GetComponent<IncreaseDiff>();
        RandomisedScript = GameObject.Find("Levels").GetComponent<RandomisedScript>();
        SP = GameObject.Find("Skill Tree").GetComponent<SkillTree>();
        animator = GetComponent<Animator>();
        speed = 5;
        basespawn.SetActive(true);
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
                if (horizontalInput == 0)
                {
                    animator.SetBool("Walking", false);
                }
                else
                {
                    animator.SetBool("Walking", true);
                }

            }

            levelDifficulty = (int)sliderValue.diffculty;
            // Code for the playere to jump when space is pressed. 
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isOnGround = false;
                animator.SetBool("PlayerJumping", true);


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

            // Checks if the right mouse button (1) is pressed down
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetBool("Defending", true);
                NODamage = true;  // Activate NODamage when defending
            }

            // Check if the right mouse button (1) is released
            if (Input.GetMouseButtonUp(1))
            {
                animator.SetBool("Defending", false);
                NODamage = false;  // Deactivate NODamage when no longer defending
            }


            // This kills the player if they reach a higght below -20.

            // Takes player out when heath gets to 0. 
            if (currentHealth <= 0)
            {
                Death();
                animator.SetBool("Defending", false);

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
        if (NODamage == true)
        {
            return;
        }
        else if (NODamage == false)
        {
            //This is the code minuses damage from the player's health. 
            currentHealth -= damage * levelDifficulty;
        }

        healthBar.SetHealth(currentHealth);
    }


    public void Death()
    {
        
        currentHealth -= maxHealth;
        RandomisedScript.DefeatedStage = 0;
        RandomisedScript.DefeatedBosses = 0;
        gameObject.SetActive(false);
        basespawn.SetActive(true);
        GameOver.SetActive(true);
        NewAdventureButton.gameObject.SetActive(true);
        animator.SetBool("Respawn", true);

    }
    public void startGame()
    {
        PlayerSpawnPoint();
        animator.SetBool("Respawn", false);
        Physics.gravity *= gravityModifier;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;

        GameIsActive = true;
        PlayerPaused = false;
        Spawned = false;
        AcentIsActive = false;
        PlayersBaseSpawnPoint.SetActive(true);

        TheHeathBar.SetActive(true);
        gameObject.SetActive(true);
        basespawn.SetActive(true);
        TitleScreen.gameObject.SetActive(false);

    }

    public void ShoppingforUpgrades()
    {
        if (beginShopping && !Shopping)
        {
            Shopping = true;
            PlayerPaused = true;
        }
        else
        {
            Shopping = false;
            PlayerPaused = false;
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
        if (other.gameObject.CompareTag("Enemy") && !NODamage)
        {
            // Tells the program to that Enemys deal 10 damage from player's health.
            Damage(10);
            animator.SetBool("PlayerWashit", true);
        }


        if (other.gameObject.CompareTag("AcentBeginner"))
        {
            startGame();
            AcentIsActive = true;
            basespawn.SetActive(false);
        }  
        
        if (other.gameObject.CompareTag("Next Level"))
        {
            if (RandomisedScript.DefeatedBosses >= 1)
            {
                TheFinalLevel.SetActive(true);
                StartNewLevel = true;
                BeginFinal = true;
            }
            else
            {
                StartNewLevel = true;
            }
        }

        if (other.gameObject.CompareTag("Shop"))
        {
            // Tells Script that the Player has begun shopping.
            Fkey.SetActive(true);
            beginShopping = true;
        }            
        
        if (other.gameObject.CompareTag("Exit"))
        {
            Leaving = true;
        }        
        if (other.gameObject.CompareTag("Diff"))
        {
            SelectDiff = true;
        }        
        
        if (other.gameObject.CompareTag("Ending"))
        {
            NearingEnding = true;
        }        
        
        if (other.gameObject.CompareTag("The End"))
        {
            EndGame = true;
        }

        if (other.gameObject.CompareTag("Death"))
        {
            // Gets destroyed if it touches the player's weapon.
            Death();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("PlayerWashit", false);
        }

        if (other.gameObject.CompareTag("Shop"))
        {
            beginShopping = false;
            Fkey.SetActive(false);

        }

        if (other.gameObject.CompareTag("Exit"))
        {
            Leaving = false;
        }

        if (other.gameObject.CompareTag("Diff"))
        {
            SelectDiff = false;
        }
    }

    // These are the functions for Skill Tree Upgrades.
    // This increases the Player's health as long as the player hasn't done 10 upgrades.
    public void increaseHeath()
    {
        // Only upgraded heath if player has upgraded heath less then 10 times. 
        if (Heathupgraded <= 9 && SP.SkillPoints >= 1)
        {
            maxHealth += 10;
            // Sends the new max health to the health bar slider. 
            healthBar.SetMaxHealth(maxHealth);
            // Adds a one to the heathUpgarded function to show the player has upgraded once again.
            Heathupgraded += 1;
        }

    }

    // This is the increase Attack function. This makes the player's attacks stronger.
    public void increaseAttack()
    {
        // Only upgrades Attack if player has upgraded attack less then 10 times. 
        if (Attackupgraded <= 9 && SP.SkillPoints >= 1)
        {
            AttackPower += 10;
            Attackupgraded += 1;
        }

    }

    // This increases the speed of the Player.
    public void increaseSpeed()
    {
        // Only upgrades speed if player has upgraded speed less then 10 times. 
        if (Speedupgraded <= 5 && SP.SkillPoints >= 1)
        {
            speed += 1;
            Speedupgraded += 1;
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


        isOnGround = true;
        animator.SetBool("PlayerJumping", false);
    }

    IEnumerator PlayerAttackCountdownRoutine()
    {
        animator.SetBool("PlayerAttacking ", true);
        NODamage = true;
        // Allows sowrd to be active for a second.
        yield return new WaitForSeconds(1);
        animator.SetBool("PlayerAttacking ", false);
        NODamage = false;
    }   
    
    IEnumerator PlayerAirAttackCountdownRoutine()
    {
        AirAttack.SetActive(true);
        NODamage = true;
        animator.SetBool("PlayerJumpAttack", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("PlayerJumpAttack", false);
        NODamage = false;
        AirAttack.SetActive(false);
    }

}
