using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBattle : MonoBehaviour
{
    // Code was taken from https://www.youtube.com/watch?v=AD4JIXQDw0s&list=PLRRnET3ZAhEzkrCdxn9Ik1xvwuwrFpPDp, due to my Enemy AI being very boring.
    Rigidbody2D rb;
    public float speed = 2.5f;
    public float attackRange = 3f;
    private GameObject player;
    private HitWeakness weakness;
    public bool isFlipped = false;
    public GameObject TheBossLevel;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        // This searches the game for an player gameobject. This is then assigned to the private GameObject player.
        player = GameObject.Find("Player");
        // The First Boss GameObject has the HitWeakness script on it. 
        weakness = GameObject.Find("First Boss").GetComponent<HitWeakness>();
    }

    // Update is called once per frame
    void Update()
    {

        Physics2D.gravity = new Vector2(0, -9.8f);
        // Gets the weakness script and sees of PlayerHitEnemyWeakness is false.
        // When the player hits the enemies weakness the enemy is meant to be frozen for a few seconds. 
        if (weakness.PlayerHitEnemyWeakness == false)
        {
            Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            LookAtPlayer();
        }

    }
    public void LookAtPlayer()
    {
        // Check if the enemy is on the left side of the player and should face right
        if (transform.position.x > player.transform.position.x && !isFlipped)
        {
            Flip();
        }
        // Check if the enemy is on the right side of the player and should face left
        else if (transform.position.x < player.transform.position.x && isFlipped)
        {   // Calls the flip function and rotates the enemy. 
            Flip();
        }
    }

    private void Flip()
    {
        // Flip the character by rotating 180 degrees on the Y axis
        transform.Rotate(0f, 180f, 0f);
        isFlipped = !isFlipped;  // Toggle the flipped state
    }
}
