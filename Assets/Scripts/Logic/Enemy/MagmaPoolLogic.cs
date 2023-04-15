using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaPoolLogic : MonoBehaviour
{
    [SerializeField]
    AudioClip magmaPoolSound;
    GameObject player;
    public int magmaPoolDamage = 10;


    private void Start()
    {
        AudioManager.PlaySound(magmaPoolSound, false, true);
    }
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            InvokeRepeating("DamagePlayer", 1f, 1f);
        }
    }

 

    private void OnTriggerExit(Collider other)
    {        
     //   Debug.Log("Player is out of Magma Pool");
        if (other.gameObject.CompareTag("Player"))
        {
            CancelInvoke("DamagePlayer");
        }
    }
   
    private void DamagePlayer()
    {
        player.GetComponent<PlayerLogic>().TakeDamage(magmaPoolDamage);
    }
}
