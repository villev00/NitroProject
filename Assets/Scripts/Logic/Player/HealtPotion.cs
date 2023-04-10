using data;
using UnityEngine;

public class HealthPotionCharge
{
    private PlayerData data;
    private PlayerUI playerUI;
    
    private void Start()
    {
        fetchPlayerData();
    }
    
    private void Update()
    {
        if (data.healthPotionCharge < 100)
        {
            data.timeSinceLastCharge += Time.deltaTime;
            if (data.timeSinceLastCharge >= data.chargeInterval)
            {
                data.timeSinceLastCharge = 0;
                ChargeHealthPotion();
                
            }
            
            
        }
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            useHealthPotion();
        }
    }
    
    
   void fetchPlayerData()
   {
       data = GameObject.Find("Player").GetComponent<PlayerData>();
       playerUI = GameObject.Find("UIManager").GetComponent<PlayerUI>();
   }
   
   // charger potion whit per second
   
    public void ChargeHealthPotion()
    {
        if(data.healthPotionCharge < 100)
        {
           // add charge to health potion
            data.healthPotionCharge += data.chargeRate;
            // update UI
            playerUI.ChangePotionText(data.healthPotionCharge);
        }
         
    }
    
    // use health potion
    
    public void useHealthPotion()
    {
        if (data.healthPotionCharge >= 100)
        {
            data.healthPotionCharge = 0;
            playerUI.ChangePotionText(data.healthPotionCharge);
            data.health += data.healthPotionCharge;
            playerUI.ChangeHealthSliderValue(data.healthPotionCharge);
        }
    }
    
   
   


  
}
