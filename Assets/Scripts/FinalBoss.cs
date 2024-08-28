using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public float EnemyHealth = 1000;


    public float speed = 5f;
    public Transform Player;
    public Animator animator;

    public bool isRunning;
    public bool Paused;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isRunning && !Paused)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            animator.SetBool("Walking", true);
            if (transform.position.x > 20)
            {
                transform.Rotate(0f, -180f, 0f);
                transform.position = new Vector2(transform.position.x, -14);
            }

            if (transform.position.x < 5)
            {
                transform.Rotate(0f, -180f, 0f);
                transform.position = new Vector3(transform.position.x, -14);
            }
        }


        float dist = Vector2.Distance(Player.position, transform.position);


        if (dist < 1)
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


        if (EnemyHealth < 0)
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
            animator.SetBool("PlayerWashit", true);
            StartCoroutine(PlayerAttackCountdownRoutine());


        }

    }

    IEnumerator PlayerAttackCountdownRoutine()
    {
        Paused = true;
        // Allows sowrd to be active for a second.
        yield return new WaitForSeconds(1);
        animator.SetBool("PlayerWashit", false);
        Paused = false;
    }
}
