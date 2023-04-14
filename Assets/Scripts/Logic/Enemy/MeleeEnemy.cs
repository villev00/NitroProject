using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public float meleeRange = 2f; // adjust this value to set the range of the melee attack
    public int meleeDamage = 10; // adjust this value to set the damage done by the melee attack
    public float timeBetweenAttacks = 1f; // adjust this value to set the time between melee attacks
    public LayerMask Player;
    bool alreadyAttacked;
    private NavMeshAgent meleeEnemy;
    public Transform player;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    Animator anim;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        meleeEnemy = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (meleeEnemy.enabled == false) return;

        meleeEnemy.SetDestination(player.position);
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
            anim.SetBool("isAttacking", true);
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
         
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
       anim.SetBool("isAttacking", false);
        anim.SetBool("isRunning", true);
       
    }
}
