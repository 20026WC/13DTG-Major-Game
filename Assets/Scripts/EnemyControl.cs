using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 800);

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
}
