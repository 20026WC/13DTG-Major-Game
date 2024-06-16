using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (transform.position.x > -12)
        {
            transform.Rotate(0f, -180f, 0f);
            transform.position = new Vector3(transform.position.x, -4, 0);
        }

        if (transform.position.x < 1)
        {
            transform.Rotate(0f, -180f, 0f);
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            //this is the damage that the enemie will do to the player when they collide. 
            Destroy(gameObject);

        }
    }

}
