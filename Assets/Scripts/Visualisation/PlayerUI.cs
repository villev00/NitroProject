using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    [SerializeField] GameObject gainMana, loseMana;
    public void ChangeHealthSliderValue(int value)
    {
        healthSlider.value += value;
    }
    public void ChangeManaSliderValue(int value)
    {
        manaSlider.value += value;
        StopAllCoroutines();
        gainMana.SetActive(false);
        loseMana.SetActive(false);
        if (value > 0)
            EnableManaIndicator(gainMana);
        else
            EnableManaIndicator(loseMana);
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
    IEnumerator EnableManaIndicator(GameObject mana)
    {
        mana.SetActive(true);
        yield return new WaitForSeconds(1f);
        mana.SetActive(false);
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
