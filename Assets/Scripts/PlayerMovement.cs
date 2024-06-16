using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;

    public float jumpForce;
    public float horizontalInput;
    public float speed = 20.0f;
    public float gravityModifier = 2.0f;
    public float playerHealth = 20f;
    public GameObject Weapon;
    public bool Attacking;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal") * speed;
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }

        if (transform.position.y < -20)
        {
            gameObject.SetActive(false);
        }

        if (playerHealth < 0)
        {
            gameObject.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(PlayerAttackCountdownRoutine());

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Attacking = false){
                playerHealth = -10;
            }

        }
    }

    IEnumerator PlayerAttackCountdownRoutine()
    {
        Weapon.SetActive(true);
        Attacking = true;
        yield return new WaitForSeconds(1);
        Weapon.SetActive(false);
        Attacking = false;
    }
}
