using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EattingEnemy : MonoBehaviour
{
    public bool Stop;
    public GameObject EnemySpawner;    
    public GameObject PlayersFirstSpawn; 
    public GameObject RunningSectionOfFight;

    private float speed = 2;
    private WhaleBossFight Whale;
    // Start is called before the first frame update
    void Start()
    {
        Whale = GameObject.Find("WhaleEnemySpawner").GetComponent<WhaleBossFight>();
        Stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Stop == false)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }

        if (Whale.WhaleisDead == true)
        {
            Destroy(gameObject);
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
            EnemySpawner.SetActive(true);
            RunningSectionOfFight.SetActive(true);

            
        }
    }
}
