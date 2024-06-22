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

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public GameObject Weapon;
    public bool Attacking;
    public bool lookingleft = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        Physics.gravity *= gravityModifier;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
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

        // Code for the playere to jump when space is pressed. 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }

        // This kills the player if they reach a higght below -20.
        if (transform.position.y < -20)
        {
            gameObject.SetActive(false);
        }

        // Takes player out when heath gets to 0. 
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        // Summons sword when player presses the left mouse key. 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Starts countdown for when to despawn sword
            StartCoroutine(PlayerAttackCountdownRoutine());
        }

    }

    void Damage(int damage)
    {
        //This is the code minuses damage from the player's health. 
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Tells the program to take 1o from player's health.
            Damage(10);
           
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
