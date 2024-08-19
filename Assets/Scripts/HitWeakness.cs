using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWeakness : MonoBehaviour
{
    public bool WeaknessWasHit = false;
    Rigidbody2D enemyRb;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            animator.SetBool("WeaknessHit", true);
            // Gets destroyed if it touches the player's weapon.
            PlayerHitWeakness();

        }
    }

    IEnumerator PlayerHitWeakness()
    {
        
        yield return new WaitForSeconds(5);
        animator.SetBool("WeaknessHit", false);
    }
}
