using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using data;
using Photon.Pun;

public class BossHealth : EnemyData
{
    PhotonView pv;
    [SerializeField] private DamageResistance damageResistance;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

        [PunRPC]
    void RPC_TakeDamage(float damage, Element element)
    {


        health -= damageResistance.CalculateDamageWithResistance(damage, element);
        if (health <= 0)
        {
            Debug.Log("Enemy Died");
            Die();
        }


    }

    public void TakeDamage(int damage)
    {
        if(pv.IsMine) pv.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }


    public void Die()
    {
        Destroy(gameObject);
    }

}
