using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
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

 
    [SerializeField] GameObject escButton, f1Button;

    bool isEscPressed, isF1Pressed;
    void ChangeAlpha()
    {
        ColorFade(escButton.GetComponent<Image>());
        ColorFade(f1Button.GetComponent<Image>());
    }
   void ColorFade(Image image)
    {
        image.color = new Color(image.color.r, image.color.g, 0, Mathf.PingPong(Time.time, 1));
    }
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

        if (!isF1Pressed &&Input.GetKeyDown(KeyCode.F1))
        {
            isF1Pressed = true;
            f1Button.gameObject.SetActive(false);
        }
       
        if(!isF1Pressed || !isEscPressed)
        ChangeAlpha();
    }
    void ToggleHowToPlay()
    {
        if (!isEscPressed)
        {
            isEscPressed = true;
            escButton.gameObject.SetActive(false);
        }
        if (!howToPlayPanel.activeSelf) howToPlayPanel.SetActive(true);
        else howToPlayPanel.SetActive(false);
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ReturnToMenu);
        Debug.Log(gameOverPanel.transform.GetChild(0).GetComponent<Button>());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void GameCompletePanel()
    {
        gameCompletePanel.SetActive(true);
        gameCompletePanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ReturnToMenu);
        Debug.Log(gameOverPanel.transform.GetChild(0).GetComponent<Button>());
        Debug.Log(gameCompletePanel.transform.GetChild(0).GetComponent<Button>());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
