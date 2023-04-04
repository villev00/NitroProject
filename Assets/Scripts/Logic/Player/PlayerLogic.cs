using System;
using System.Collections.Generic;
using data;
using UnityEngine;

public class PlayerLogic : MonoBehaviour

{
    public GameObject flameBarrier;
    [SerializeField]
    PlayerData data = new PlayerData();
    
   
    public void TakeDamage(int damage)
    {
        if (flameBarrier != null)
        {
            data.shield -= damage;
            if (data.shield < 0)
            {
                data.health += data.shield;
                data.shield = 0;
                Destroy(flameBarrier);
            }
        }
        else
        {
            data.health -= damage;
        }

        if (data.health <= 0)
        {
            Die();
        }
    }
    public void SetShieldValue(int value)
    {
        data.shield = value;
    }
    public void Heal(int heal)
    {
        data.health += heal;
        if (data.health > data.maxHealth)
        {
            data.health = data.maxHealth;
        }
    }
    public int GetMana()
    {
        return data.mana;
    }

    public void SetMana(int amount)
    {
        data.mana += amount;
        if (data.mana > data.maxMana)
        {
            data.mana = data.maxMana;
        }
        if (data.mana < 0)
        {
            data.mana = 0;
        }
    }
    public IEnumerable<WaitForSeconds> Tickdamage(int damage, int tickrate , int duration) 
    {
        
        for (int i = 0; i < duration; i++)
        {
            TakeDamage(damage);
            Debug.Log("Player took " + damage + " damage");
               yield return new WaitForSeconds(tickrate);
           
        }
        
        
    }
    
    public IEnumerable<WaitForSeconds>  Healtick(int heal, int tickrate , int duration) 
    {
        
        for (int i = 0; i < duration; i++)
        {
            Heal(heal);
            Debug.Log("Player healed " + heal + " health");
            yield return new WaitForSeconds(tickrate);
        }
        
        
    }

    public void Die()
    {
        Debug.Log("Player died");
        LoseLife(1);
    }
    
    public void LoseLife(int lives)
    {
        data.lives -= 1;
        if (data.lives <= 0)
        {
            GameOver();
        }
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over");
    }
    
    

}