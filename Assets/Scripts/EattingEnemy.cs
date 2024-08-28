using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EattingEnemy : MonoBehaviour
{
    public bool Stop = false;   

    // This gets the PlayersFirst spawn so that the script can deleetd it when the time is right. 
    public GameObject PlayersFirstSpawn; 
    public GameObject RunningSectionOfFight;

    // The speed of this eenmy is 5.
    private float speed = 5;

    // Update is called once per frame
    void Update()
    {
        // If stop isn't true then the enemy will move to the right at a speed of 5 until they reach the stoip trigger to start the second part of the boss fight agaienst the giant enemy.
        if (Stop == false)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            // this detroys the floor when this enemyt toaches it.
            Destroy(other.gameObject);
            // This destroys the player's spawn point once this mdoel toaches it. The first phase of this boss fight has the player running away from the enemy.
            Destroy(PlayersFirstSpawn.gameObject);
        }
        
        if (other.gameObject.CompareTag("BeginGiantBattle"))
        {
            // If the player toaches the BeginGiantBattle trigger the enemy stops and destroys the asserts used for the running section of the game. 
            Stop = true;
            Destroy(RunningSectionOfFight.gameObject);

            
        }
    }
}
