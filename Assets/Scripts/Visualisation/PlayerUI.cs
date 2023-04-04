using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    Slider healthSlider, manaSlider;
    void ChangeHealthSliderValue(int value)
    {
        healthSlider.value += value;
    }
    void ChangeManaSliderValue(int value)
    {
        manaSlider.value += value;
    }
}
