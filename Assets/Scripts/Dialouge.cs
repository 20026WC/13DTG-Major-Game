using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialouge : MonoBehaviour
{
    private PlayerMovement Player;
    private Exit exit;


    public bool PlayerHasReadFirstMessage;
    public bool PlayerHasReadLastMessage;
    public bool PlayerHasReadFinalMessage;
    public bool BeginFinalBossFight = false;
    public bool GameHasEnded = false;

    public GameObject PlayersFirstSpawn;

    // All the texts that appear in the beginign of the game. 
    public GameObject Textbox;
    public GameObject FirstText;
    public GameObject SecondText;
    public GameObject ThirdText;
    public GameObject FourthText;
    public GameObject FifthText;  
    public GameObject SixthText;
    public GameObject SeventhText;    
    

    // All the text that appears just before the final boss fight. 
    public GameObject LastFirstText;
    public GameObject LastSecondText;
    public GameObject LastThirdText;
    public GameObject LastFourthText;
    public GameObject LastFifthText;
    public GameObject LastSixthText;

    // This is alk the text that appears in the Final scene of the game.
    public GameObject FinalFirstText;
    public GameObject FinalSecondText;
    public GameObject FinalThirdText;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        exit = GameObject.Find("Exit").GetComponent<Exit>();
        // This code is so that the player only has to read all the tetxs once and never again. 
        // These would be set to active once the player has read all the text in that section. 
        PlayerHasReadFirstMessage = false;
        PlayerHasReadLastMessage = false;
    }

    // Update is called once per frame
    void Update()
    {
        // as seen in these codes, the player steps on diffeent triggers and are taken to different messages.
        // ! menas not, so all these scripts are saying to omly show the player the messages if they have not read the message yet. 
        if (Player.GameIsActive && !PlayerHasReadFirstMessage)
        {
            StartCoroutine(BeginningOfGame());
        }  
        if (Player.NearingEnding && !PlayerHasReadLastMessage)
        {
            StartCoroutine(EndingOfGamePart1());
        }
        if (Player.EndGame && !PlayerHasReadFinalMessage)
        {
            StartCoroutine(EndingOfGamePart2());
        }
    }

    // These are the code for which text should be visble or not.
    IEnumerator BeginningOfGame()
    {
        Textbox.SetActive(true);
        // this teels the script that the player has read the text so to not show it to them again.
        PlayerHasReadFirstMessage = true;
        // This freezes the Player's posistion so that they do cannot move when the text boxes appear. 
        Player.PlayerPaused = true;
        FirstText.SetActive(true);
        yield return new WaitForSeconds(2);
        FirstText.SetActive(false);
        SecondText.SetActive(true);
        yield return new WaitForSeconds(2);
        SecondText.SetActive(false);
        ThirdText.SetActive(true);
        yield return new WaitForSeconds(2);
        ThirdText.SetActive(false);
        FourthText.SetActive(true);
        yield return new WaitForSeconds(2);
        FourthText.SetActive(false);
        FifthText.SetActive(true);
        yield return new WaitForSeconds(1);
        FifthText.SetActive(false);
        SixthText.SetActive(true);
        yield return new WaitForSeconds(4);
        SixthText.SetActive(false);
        SeventhText.SetActive(true);
        yield return new WaitForSeconds(1);
        SeventhText.SetActive(false);
        // This gives the player their mobility back. 
        Player.PlayerPaused = false;
        // This makes the text box invibale so it doesn't block the player's view of the game.
        Textbox.SetActive(false);


    }    
    IEnumerator EndingOfGamePart1()
    {

        Textbox.SetActive(true);
        PlayerHasReadLastMessage = true;
        Player.PlayerPaused = true;
        LastFirstText.SetActive(true);
        yield return new WaitForSeconds(2);
        LastFirstText.SetActive(false);
        LastSecondText.SetActive(true);
        yield return new WaitForSeconds(2);
        LastSecondText.SetActive(false);
        LastThirdText.SetActive(true);
        yield return new WaitForSeconds(2);
        LastThirdText.SetActive(false);
        LastFourthText.SetActive(true);
        yield return new WaitForSeconds(2);
        LastFourthText.SetActive(false);
        LastFifthText.SetActive(true);
        yield return new WaitForSeconds(1);
        LastFifthText.SetActive(false);
        LastSixthText.SetActive(true);
        yield return new WaitForSeconds(1);
        LastSixthText.SetActive(false);
        Player.PlayerPaused = false;
        Textbox.SetActive(false);
        // This tells the otehr scripts to sommon the final boss. 
        BeginFinalBossFight = true;
    }   
    
    IEnumerator EndingOfGamePart2()
    {
        Textbox.SetActive(true);
        PlayerHasReadFinalMessage = true;
        Player.PlayerPaused = true;
        FinalFirstText.SetActive(true);
        yield return new WaitForSeconds(2);
        FinalFirstText.SetActive(false);
        FinalSecondText.SetActive(true);
        yield return new WaitForSeconds(2);
        FinalSecondText.SetActive(false);
        FinalThirdText.SetActive(true);
        yield return new WaitForSeconds(5);
        FinalThirdText.SetActive(false);
        Textbox.SetActive(false);
        GameHasEnded = true;
        // This resrtas the game so that the player is taken back to the title screen. 
        exit.RestartGame();


    }
}
