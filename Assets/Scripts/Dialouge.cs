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

    public GameObject Textbox;
    public GameObject FirstText;
    public GameObject SecondText;
    public GameObject ThirdText;
    public GameObject FourthText;
    public GameObject FifthText;  
    public GameObject SixthText;
    public GameObject SeventhText;    
    

    public GameObject LastFirstText;
    public GameObject LastSecondText;
    public GameObject LastThirdText;
    public GameObject LastFourthText;
    public GameObject LastFifthText;
    public GameObject LastSixthText;

    public GameObject FinalFirstText;
    public GameObject FinalSecondText;
    public GameObject FinalThirdText;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        exit = GameObject.Find("Exit").GetComponent<Exit>();
        PlayerHasReadFirstMessage = false;
        PlayerHasReadLastMessage = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    IEnumerator BeginningOfGame()
    {
        Textbox.SetActive(true);
        PlayerHasReadFirstMessage = true;
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
        Player.PlayerPaused = false;
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
        yield return new WaitForSeconds(2);
        exit.RestartGame();


    }
}
