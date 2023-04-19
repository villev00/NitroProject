using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public Animator animator;

    public int trapDamage;

    void Start()
    {
        animator = GetComponent<Animator>(); 
        InvokeRepeating("WallSpikeTrap", 0f, 4f);
        //InvokeRepeating("FloorSpikeTrap", 0f, 5f);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit by trap");
            collision.gameObject.GetComponent<PlayerLogic>().TakeDamage(trapDamage);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(trapDamage, 0);
        }
    }

    public void WallSpikeTrap()
    {
        if(!animator.GetBool("WallSpikeTrap"))
        {
            animator.SetBool("WallSpikeTrap", true);
        }
        else
        {
            animator.SetBool("WallSpikeTrap", false);
        }
       
    }
    public void FloorSpikeTrap()
    {
       // animator.SetBool("FloorSpikeTrap", true);
    }
}
