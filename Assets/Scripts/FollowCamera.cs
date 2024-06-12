using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 4, -10);
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
