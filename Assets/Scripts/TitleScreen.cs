using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject StartGamePack;
    public GameObject ControlsMenu;
    // Start is called before the first frame update
    void Start()
    {
        CloseControlMenu();
    }

    public void OpenControlMenu()
    {
        StartGamePack.SetActive(false);
        ControlsMenu.SetActive(true);

    }

    public void CloseControlMenu()
    {
        StartGamePack.SetActive(true);
        ControlsMenu.SetActive(false);
    }
}
