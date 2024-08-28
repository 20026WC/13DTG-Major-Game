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
            // If the player is on the leaving trigger then F will appear to indicate top press that.
            F.SetActive(true);


            if (Input.GetKeyDown(KeyCode.F))
            {
                // This tells the script that to set up the dialouge for if the player willing leaevs the tree.
                StartCoroutine(PlayerHasLefttree());
            }
        }
        else
        {
            // If the player exist the trigger than F will be invisable.
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
        // This turns the respawn button invisible so it doesn't block the text.  
        Respawn.SetActive(false);
        TextBox.SetActive(true);
        DeathTextBox.SetActive(true);
        yield return new WaitForSeconds(1);
        RestartGame();
    }

    // This function resets the game back to the tile screen so the player cna play the game from the start.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
