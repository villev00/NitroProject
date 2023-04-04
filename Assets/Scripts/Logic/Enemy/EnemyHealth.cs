using System.Collections;
using System.Collections.Generic;
using data;
using UnityEngine;

public class EnemyHealth : EnemyData
{
    
    public void TakeDamage(int damage)
    {
      
        
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        

    }
    
    
    public  void Die()
    {
        Destroy(gameObject);
    }
    
    

}
