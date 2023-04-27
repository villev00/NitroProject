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
    // Damage taken from Spell
    public void TakeDamage(float damage, Element element)
    {
        
        if (element == Element.Lightning)
        {
            StartCoroutine(Stun());
        }

        health -= DamageTaken(damage, element); //damageResistance.CalculateDamageWithResistance(damage, element);
        
        if (health <= 0)
        {
            FloatingCombatText.Create(transform.position, DamageTaken(damage, element),true);
            headCollider.enabled = false;
            bodyCollider.enabled = false;
            StartCoroutine(Die());
        }
        else
        {
            FloatingCombatText.Create(transform.position, DamageTaken(damage, element),false);
        }
    }

    // Damage taken from basic attack
    public void TakeDamage(float damage, Element element, Vector3 position, bool headshot)
    {

        if (element == Element.Lightning)
        {
            StartCoroutine(Stun());
        }

        health -= DamageTaken(damage, element); //damageResistance.CalculateDamageWithResistance(damage, element);
        
        if (health <= 0)
        {
            FloatingCombatText.Create(position, DamageTaken(damage, element), headshot);
            headCollider.enabled = false;
            bodyCollider.enabled = false;
            StartCoroutine(Die());
        }
        else
        {
            FloatingCombatText.Create(position, DamageTaken(damage, element), headshot);
        }
    }
    float DamageTaken(float incomingDamange, Element currentElement)
    {
        float newDamageTaken;
        newDamageTaken = damageResistance.CalculateDamageWithResistance(incomingDamange, currentElement);
        return newDamageTaken;
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
        // get nearst enemySpawner and remove this enemy from the list

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
