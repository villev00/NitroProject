using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public float meleeRange = 2f; 
    public int meleeDamage = 10; 
    public float timeBetweenAttacks = 1f; 
    public LayerMask Player;
    bool alreadyAttacked;
    private NavMeshAgent meleeEnemy;
    public Transform player;
    public ParticleSystem lightningFX;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    Animator anim;
    EnemyHealth enemyHealth;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        meleeEnemy = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (meleeEnemy.enabled == false) return;

        if (!enemyHealth.isStunned)
        {
            meleeEnemy.SetDestination(player.position);
            anim.enabled = true;
            anim.SetBool("isRunning", true);
            lightningFX.gameObject.SetActive(false); // disable the Lightning FX component
        }
        else
        {
            meleeEnemy.SetDestination(transform.position);
            anim.enabled = false;
            lightningFX.gameObject.SetActive(true); // enable the Lightning FX component
        }
       
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);
        if (playerInAttackRange && playerInSightRange) StartAttack();
    }

    void StartAttack()
    {
        if (!anim.GetBool("isAttacking"))
        {
            anim.SetBool("isRunning", false);

            if (!enemyHealth.isStunned)
            {
                anim.SetBool("isAttacking", true);
            }
            else
            {
                StartCoroutine(ResetAttack());
            }
        }

    }
    private void AttackPlayer()
    {
       
        meleeEnemy.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // check if the player is within range for a melee attack
            if (Vector3.Distance(transform.position, player.position) <= meleeRange)
            {
                // apply damage to the player
                player.GetComponent<PlayerLogic>().TakeDamage(meleeDamage);
                anim.SetBool("isRunning", false);
                anim.SetBool("isAttacking", true);
            }

            alreadyAttacked = true;

            StartCoroutine(ResetAttack());
        }
    }
    private IEnumerator ResetAttack()
    {
        alreadyAttacked = false;
        anim.SetBool("isAttacking", false);
        anim.SetBool("isRunning", true);
        yield return new WaitForSeconds(timeBetweenAttacks);

    }
}
