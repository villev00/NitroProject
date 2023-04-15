using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShootingLogic : MonoBehaviour
{
 
    public int rangedDamage = 5;

   
    private void OnTriggerEnter(Collider other)
    {
        //damage player
        if (other.gameObject.CompareTag("Player"))
        {                          
            other.gameObject.GetComponent<PlayerLogic>().TakeDamage(rangedDamage);
            Destroy(gameObject);
        }

    }
}
