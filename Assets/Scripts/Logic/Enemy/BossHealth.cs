using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using data;
using Photon.Pun;
using static RuneData;

public class BossHealth : EnemyData
{
    PhotonView pv;
    [SerializeField] private DamageResistance damageResistance;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

        [PunRPC]
    void RPC_TakeDamage(float damage)
    {


        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Enemy Died");
            if (pv.IsMine) pv.RPC("RPC_Die", RpcTarget.All);
        }


    }

    public void TakeDamage(float damage, Element element)
    {
      //  damage = damageResistance.CalculateDamageWithResistance(damage, element);
        pv.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_Die()
    {
        Destroy(gameObject);
    }

}
