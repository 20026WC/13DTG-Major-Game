using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseDiff : MonoBehaviour
{
    public Slider slider;
    public float diffculty;

    // Update is called once per frame
    void Update()
    {
        slider.onValueChanged.AddListener(delegate { CheckSliderValues(); });
    }

    public void CheckSliderValues()
    {
        diffculty = slider.value;

        if (slider.value == 1)
        {
            Debug.Log("Easy");
        }
        else if (slider.value == 2)
        {
            Debug.Log("Normal");
        }
        else if (slider.value == 3)
        {
            Debug.Log("Hard");
        }
        else
        {
            Debug.Log("Nothing");
        }
    }
}
