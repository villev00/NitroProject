using System.Collections;
using System.Collections.Generic;
using data;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : EnemyData
{
    public float stunDuration = 100f;

    Animator anim;
    public bool isStunned = false;
    [SerializeField] private DamageResistance damageResistance;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage, Element element)
    {
        if (element == Element.Lightning)
        {
            StartCoroutine(Stun());
        }

        health -= damageResistance.CalculateDamageWithResistance(damage, element);
        if (health <= 0)
        {
            Debug.Log("Enemy Died");
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
