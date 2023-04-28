using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using data;
using Photon.Pun;
using static RuneData;

public class BossHealth : BossData
{
    [SerializeField]
    GameObject flameBarrier;
    BossUI bossUI;
    PhotonView pv;
    [SerializeField] private DamageResistance damageResistance;
    Animator anim;
    private int wait = 10;
    private bool isEnraged = false;
    public bool isDead = false;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        bossUI = GameObject.Find("Managers").transform.GetChild(1).GetComponent<BossUI>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (health < 1000)
        {
            Enrage();
        }
    }

    [PunRPC]
    void RPC_TakeDamage(float damage, Element element)
    {
        health -= DamageTaken(damage, element);
        bossUI.ChangeHealthSliderValue(health);
        if (health <= 0)
        {          
            Debug.Log("Enemy Died");
            StartCoroutine(DieAnimation());
        }
    }

    // Damage taken from spells
    public void TakeDamage(float damage, Element element)
    {
        //damage = damageResistance.CalculateDamageWithResistance(damage, element);
        pv.RPC("RPC_TakeDamage", RpcTarget.All, damage, element);
        FloatingCombatText.Create(transform.position, damage, false);
    }

    // Damage taken from basic attack
    public void TakeDamage(float damage, Element element, Vector3 position, bool headshot)
    {
        // damage = damageResistance.CalculateDamageWithResistance(damage, element);
        pv.RPC("RPC_TakeDamage", RpcTarget.All, damage, element);
        FloatingCombatText.Create(position, damage, headshot);
    }
    float DamageTaken(float incomingDamange, Element currentElement)
    {
        float newDamageTaken;
        if (isEnraged)
        {
            newDamageTaken = damageResistance.CalculateDamageWithResistance(incomingDamange, currentElement);
        }
        else
        {
            newDamageTaken = incomingDamange;
        }
        
        return newDamageTaken;
    }
    private IEnumerator DieAnimation()
    {
        isDead = true;
        anim.SetTrigger("die");
        yield return new WaitForSeconds(wait);
        if (pv.IsMine) pv.RPC("RPC_Die", RpcTarget.All);
    }

    public void Enrage()
    {

        isEnraged = true;
        flameBarrier.SetActive(true);
        GetComponent<UnityEngine.AI.NavMeshAgent>().speed *= 1.5f; // Increase the boss's speed by 50%
    }

    [PunRPC]
    void RPC_Die()
    {
        Destroy(gameObject);
        GameObject.Find("UIManager").GetComponent<PlayerUI>().GameCompletePanel();
        Time.timeScale = 0;
    }

}
