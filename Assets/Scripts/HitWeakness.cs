using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWeakness : MonoBehaviour
{

    public Animator animator;
    public GameObject NotHitState;
    public bool PlayerHitEnemyWeakness = false;
    private BossDamage death;
    private RandomisedScript RandomisedScript;
    private PlayerMovement Player;
    public GameObject EndLevelDoor;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        death = GameObject.Find("Head area").GetComponent<BossDamage>();
        RandomisedScript = GameObject.Find("Levels").GetComponent<RandomisedScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // This destroys the entire model when the enemies dies.
        if (death.BossisDead == true)
        {
            AfterMathOfbATTLE();

        }

        if (transform.position.y < -70)
        {
            Destroy(gameObject);
        }
    }

    public void AfterMathOfbATTLE()
    {       
        Destroy(gameObject);
        EndLevelDoor.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player hits the enemy's weakness it begins the enemies WeaknessHit animation. 
        if (other.gameObject.CompareTag("Weapon"))
        {
            // This disables the enemies NotHitState. The enemies NoHitSpace is the enemies' triggers which damage the Player. 

            death.Damage(10);
            StartCoroutine(CountdownRoutine());


        }
    }

    IEnumerator CountdownRoutine()
    {
        NotHitState.SetActive(false);
        animator.SetBool("WeaknessHit", true);
        PlayerHitEnemyWeakness = true;
        yield return new WaitForSeconds(2);
        animator.SetBool("WeaknessHit", false);
        // This reengages the enemies triggers which damage the Player. 
        NotHitState.SetActive(true);
        PlayerHitEnemyWeakness = false;
    }


}
