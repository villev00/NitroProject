using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    Slider healthSlider, manaSlider, healthPotionSlider;

    [SerializeField]
    GameObject lives, lifePrefab;

    [SerializeField]
    TMPro.TextMeshProUGUI PotionText;

    [SerializeField] GameObject howToPlayPanel, gameOverPanel, gameCompletePanel;
    public void ChangeHealthSliderValue(int value)
    {
        healthSlider.value += value;
    }
    public void ChangeManaSliderValue(int value)
    {
        manaSlider.value += value;
      
    }
    public void ChangeHealthPotionValue(int value)
    {
        healthPotionSlider.value += value;
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

    public void ChangePotionText(int amount)
    {
        PotionText.text = amount.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ToggleHowToPlay();
    }
    void ToggleHowToPlay()
    {
        if (!howToPlayPanel.activeSelf) howToPlayPanel.SetActive(true);
        else howToPlayPanel.SetActive(false);
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ReturnToMenu);
    }
    public void GameCompletePanel()
    {
        gameCompletePanel.SetActive(true);
        gameCompletePanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ReturnToMenu);
    }
    void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
