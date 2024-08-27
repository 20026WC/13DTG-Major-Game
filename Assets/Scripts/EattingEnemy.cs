using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EattingEnemy : MonoBehaviour
{
    public bool Stop;   
    public GameObject PlayersFirstSpawn; 
    public GameObject RunningSectionOfFight;

    private float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        Stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Stop == false)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Destroy(other.gameObject);
            Destroy(PlayersFirstSpawn.gameObject);
        }
        
        if (other.gameObject.CompareTag("BeginGiantBattle"))
        {
            Stop = true;
            Destroy(RunningSectionOfFight.gameObject);

            
        }
    }
}
