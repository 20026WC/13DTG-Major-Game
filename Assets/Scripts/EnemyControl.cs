using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    Rigidbody2D enemyRb;
    public float speed;
    public float enemyHealth = 20;
    private GameObject player;
    public GameObject Cube;
    private PlayerMovement PlayerMovement;
    private SpawnManager Spawner;
    private SkillTree SP;
    private float powerupStrength = 20000000f;
    public bool Knockback;

    public Color HitColor;
    public Color NormalColor;
    private SpriteRenderer rend;

    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        SP = GameObject.Find("Skill Tree").GetComponent<SkillTree>();

        Spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        rend = GetComponent<SpriteRenderer>();
        

        enemyHealth = enemyHealth * PlayerMovement.levelDifficulty;
    }

    void Update()
    {
        Vector2 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (enemyHealth < 0)
        {
            Destroy(gameObject);
            SP.SkillPoints += 1;

        }

        if (!PlayerMovement.AcentIsActive)
        {
            death();
        }


        if (transform.position.y < -40)
        {
            death();
        }

        if (Spawner.PlayerIsDead == true)
        {
            death();
        }


        
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
            StartCoroutine(CountdownRoutine());

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
        rend.color = HitColor;
        yield return new WaitForSeconds(2);
        rend.color = NormalColor;
    }
}
