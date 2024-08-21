using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWeakness : MonoBehaviour
{
    public bool WeaknessWasHit;

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
            

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Weapon"))
        {
            PlayerHitWeakness();
            animator.SetBool("WeaknessHit", false);

        }
    }
    IEnumerator PlayerHitWeakness()
    {
        WeaknessWasHit = true;
        yield return new WaitForSeconds(10);
        WeaknessWasHit = false;
    }
}
