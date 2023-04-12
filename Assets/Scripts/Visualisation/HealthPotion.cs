using data;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    PhotonView pv;
    PlayerLogic pLogic;
    public PlayerUI playerUI;


    int chargeRate = 5;
    float chargeInterval = 2f;
    float timeSinceLastCharge = 0f;
    int healthPotionCharge = 100;
    int amountToHeal;
    int maxCharge=100;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
      
       
    }
    private void Start()
    {
        if (pv.IsMine)
        {
            playerUI = GameObject.Find("UIManager").GetComponent<PlayerUI>();
            pLogic = GetComponent<PlayerLogic>();
        }

    }

    private void Update()
    {
        if (!pv.IsMine) return;
        if (healthPotionCharge < 100)
        {
            timeSinceLastCharge += Time.deltaTime;
            if (timeSinceLastCharge >= chargeInterval)
            {
                timeSinceLastCharge = 0;
                ChargeHealthPotion();

            }


        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            UseHealthPotion();
        }
    }


  
    // charger potion whit per second

    public void ChargeHealthPotion()
    {
        if (healthPotionCharge < 100)
        {
            // add charge to health potion
            healthPotionCharge += chargeRate;
            if (healthPotionCharge > maxCharge) healthPotionCharge = maxCharge;
            // update UI
            playerUI.ChangeHealthPotionValue(chargeRate);
        }

    }

    // use health potion

    void UseHealthPotion()
    {
        int currentHealth = pLogic.GetHealth();
        amountToHeal = 20 ;
        healthPotionCharge -= amountToHeal;
        pLogic.Heal(amountToHeal);
        playerUI.ChangeHealthPotionValue(-amountToHeal);
        
    }




}
