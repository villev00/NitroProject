using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossUI : MonoBehaviour
{
    [SerializeField]
    Slider BossHealth;
    public void ChangeHealthSliderValue(float value)
    {
        BossHealth.value += value;
    }
}
