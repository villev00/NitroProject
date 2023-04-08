using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    Slider healthSlider, manaSlider;

    [SerializeField]
    GameObject lives, lifePrefab;


    public void ChangeHealthSliderValue(int value)
    {
        healthSlider.value += value;
    }
    public void ChangeManaSliderValue(int value)
    {
        manaSlider.value += value;
      
    }

    public void ChangeLives(int amount)
    {
        if (amount != 1)
        {
            for (int i = 0; i < amount; i++)
                Instantiate(lifePrefab, lives.transform);
        }
        else
        {
            int livesLeft = lives.transform.childCount;
            Destroy(lives.transform.GetChild(livesLeft - 1).gameObject);
        }
        
    }
}
