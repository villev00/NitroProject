using System.Collections;
using System.Collections.Generic;
using data;
using Data;
using Logic.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : EnemyData
{
    public float stunDuration;

    Animator anim;
    public bool isStunned = false;
    [SerializeField] 
    private DamageResistance damageResistance;
    [SerializeField]
    Collider headCollider;
    [SerializeField] 
    Collider bodyCollider;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        stunDuration = 2f;
    }

    public void TakeDamage(float damage, Element element)
    {
        
        if (element == Element.Lightning)
        {
            StartCoroutine(Stun());
        }

        health -= damageResistance.CalculateDamageWithResistance(damage, element);
        //FloatingCombatText.Create(transform.position, damageResistance.CalculateDamageWithResistance(damage, element));
        if (health <= 0)
        {
            headCollider.enabled = false;
            bodyCollider.enabled = false;
            StartCoroutine(Die());
        }
    }

    IEnumerator Stun()
    {
        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        // end of stun
        isStunned = false;
    }

    IEnumerator Die()
    {
        anim.SetBool("isDead", true);
        GetComponent<NavMeshAgent>().enabled = false;
        

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
