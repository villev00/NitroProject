using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using data;
using Photon.Pun;
using static RuneData;

public class BossHealth : EnemyData
{
    BossUI bossUI;
    PhotonView pv;
    [SerializeField] private DamageResistance damageResistance;
    Animator anim;
    private int wait = 10;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        bossUI = GameObject.Find("Managers").transform.GetChild(1).GetComponent<BossUI>();
        anim = GetComponentInChildren<Animator>();
    }

        [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        health -= damage;
        bossUI.ChangeHealthSliderValue(-damage);
        if (health <= 0)
        {          
            Debug.Log("Enemy Died");
            StartCoroutine(DieAnimation());
        }
    }

    // Damage taken from spells
    public void TakeDamage(float damage, Element element)
    {
        //  damage = damageResistance.CalculateDamageWithResistance(damage, element);
        pv.RPC("RPC_TakeDamage", RpcTarget.All, damage);
        FloatingCombatText.Create(transform.position, damage);
    }

    // Damage taken from basic attack
    public void TakeDamage(float damage, Element element, Vector3 position)
    {
        //  damage = damageResistance.CalculateDamageWithResistance(damage, element);
        pv.RPC("RPC_TakeDamage", RpcTarget.All, damage);
        FloatingCombatText.Create(position, damage);
    }

    private IEnumerator DieAnimation()
    {
        anim.SetTrigger("die");
        yield return new WaitForSeconds(wait);
        if (pv.IsMine) pv.RPC("RPC_Die", RpcTarget.All);
    }

    [PunRPC]
    void RPC_Die()
    {
        Destroy(gameObject);
        GameObject.Find("UIManager").GetComponent<PlayerUI>().GameCompletePanel();
        Time.timeScale = 0;
    }

}
