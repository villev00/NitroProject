using System.Collections;
using System.Collections.Generic;
using data;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : EnemyData
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
      
        
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Enemy Died");
            StartCoroutine(Die());
        }
        

    }
    
    
    IEnumerator Die()
    {
        anim.SetBool("isDead", true);
        GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
    
    

}
