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
        horizontalInput = Input.GetAxis("Horizontal") * speed;
        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.A) && !lookingleft)
        {
            transform.Rotate(0f, 180f, 0f);
            lookingleft = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && lookingleft)
        {
            transform.Rotate(0f, -180f, 0f);
            lookingleft = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }

        if (transform.position.y < -20)
        {
            gameObject.SetActive(false);
        }

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(PlayerAttackCountdownRoutine());

        }

    }

    void Damage(int damage)
    {
        //This is the code for damage is minused form the player's health. 
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Damage(10);
           
        }
    }

    IEnumerator PlayerAttackCountdownRoutine()
    {
        Weapon.SetActive(true);
        Attacking = true;
        yield return new WaitForSeconds(1);
        Weapon.SetActive(false);
        Attacking = false;
    }
}
