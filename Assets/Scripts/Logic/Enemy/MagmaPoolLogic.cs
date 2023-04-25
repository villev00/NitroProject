using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaPoolLogic : MonoBehaviour
{
    [SerializeField]
    AudioClip magmaPoolSound;
    GameObject player;
    PhotonView pv;
    public int magmaPoolDamage = 10;
    public float magmaPoolDuration = 10f;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        AudioManager.PlaySound(magmaPoolSound, false, true);
        Invoke("DestroyMagmaPool", magmaPoolDuration);
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
    void DestroyMagmaPool()
    {
        if (pv.IsMine) pv.RPC("RPC_DestroyMagmaPool", RpcTarget.All);
    }
    void RPC_DestroyMagmaPool()
    {
        Destroy(gameObject);
    }
}
