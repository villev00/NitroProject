using System;
using System.Collections.Generic;
using data;
using UnityEngine;

public class HealthLogic : PlayerData

{
    public GameObject flameBarrier;

    public void TakeDamage(int damage)
    {
        if (flameBarrier != null)
        {
            shield -= damage;
            if (shield < 0)
            {
                health += shield;
                shield = 0;
                Destroy(flameBarrier);
            }
        }
        else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth)
        {
            health = maxHealth;
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
        loseLife(1);
    }
    
    public void loseLife(int lives)
    {
        lives -= 1;
        if (lives <= 0)
        {
            GameOver();
        }
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over");
    }
    
    

}