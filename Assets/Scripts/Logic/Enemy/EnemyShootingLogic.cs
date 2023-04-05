using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShootingLogic : MonoBehaviour
{
    PlayerLogic pLogic;
    public int rangedDamage = 15;

    void Awake()
    {
        pLogic= GetComponent<PlayerLogic>();
    }
    private void OnCollisionEnter(Collision other)
    {
        //damage player
        if (other.gameObject.CompareTag("Player"))
        {                          
                other.gameObject.GetComponent<PlayerLogic>().TakeDamage(rangedDamage);                               
        }

    }
}
