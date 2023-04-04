using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    Slider healthSlider, manaSlider;
    public void ChangeHealthSliderValue(int value)
    {
        healthSlider.value += value;
        if (healthSlider.value > healthSlider.maxValue)
        {
            healthSlider.value = healthSlider.maxValue;
        }
        if (healthSlider.value < healthSlider.minValue)
        {
            healthSlider.value = healthSlider.minValue;
        }
    }
   public  void ChangeManaSliderValue(int value)
    {
        manaSlider.value += value;
        if (manaSlider.value > manaSlider.maxValue)
        {
            manaSlider.value = manaSlider.maxValue;
        }
        if (manaSlider.value < manaSlider.minValue)
        {
            manaSlider.value = manaSlider.minValue;
        }
    }
    
}
