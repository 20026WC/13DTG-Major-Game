using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialouge : MonoBehaviour
{
    private PlayerMovement Player;
    public bool PlayerHasReadFirstMessage;
    public bool PlayerHasReadLastMessage;
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
    public GameObject LastSeventhText;
    public GameObject LasEighthText;

    public GameObject FinalBoss;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
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
            StartCoroutine(EndingOfGame());
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
    IEnumerator EndingOfGame()
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
        LastSeventhText.SetActive(true);
        yield return new WaitForSeconds(1);
        LastSeventhText.SetActive(false);
        LasEighthText.SetActive(true);
        yield return new WaitForSeconds(1);
        LasEighthText.SetActive(false);
        Player.PlayerPaused = false;
        Textbox.SetActive(false);
        FinalBoss.SetActive(true);


    }
}
