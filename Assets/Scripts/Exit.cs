using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private PlayerMovement Player;

    public GameObject F;
    public GameObject TextBox;
    public GameObject ExitText;
    public GameObject ExitText2;
    
    
    public GameObject DeathTextBox;
    public GameObject Respawn;



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Leaving == true)
        {
            F.SetActive(true);


            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(PlayerHasLefttree());
            }
        }
        else
        {
            F.SetActive(false);
        }
    }
    public void WillinglyDied()
    {
        StartCoroutine(playerWillingDied());
    }

    IEnumerator PlayerHasLefttree()
    {
        TextBox.SetActive(true);
        Player.PlayerPaused = true;
        ExitText.SetActive(true);
        yield return new WaitForSeconds(1);
        ExitText2.SetActive(true);
        yield return new WaitForSeconds(1);
        RestartGame();
    }   
    
    IEnumerator playerWillingDied()
    {
        Respawn.SetActive(false);
        TextBox.SetActive(true);
        DeathTextBox.SetActive(true);
        yield return new WaitForSeconds(1);
        RestartGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
