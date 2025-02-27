using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseDiff : MonoBehaviour
{
    public Slider slider;
    public float diffculty;
    [SerializeField] private TMP_Text _title;

    private PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.onValueChanged.AddListener(delegate { CheckSliderValues(); });

        if (PlayerMovement.GameIsActive) 
        {
            slider.gameObject.SetActive(false);
        }


        if (PlayerMovement.SelectDiff)
        {
            slider.gameObject.SetActive(true);
        }

    }

    public void CheckSliderValues()
    {
        diffculty = slider.value;

        if (slider.value == 1)
        {
            Debug.Log("Easy");
            _title.text = "Easy";
        }
        else if (slider.value == 2)
        {
            Debug.Log("Normal");
            _title.text = "Normal";
        }
        else if (slider.value == 3)
        {
            Debug.Log("Hard");
            _title.text = "Hard";
        }
        else
        {
            Debug.Log("Nothing");
        }
    }
}
