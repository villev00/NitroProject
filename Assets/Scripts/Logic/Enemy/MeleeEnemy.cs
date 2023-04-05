using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public float meleeRange = 2f; // adjust this value to set the range of the melee attack
    public int meleeDamage = 10; // adjust this value to set the damage done by the melee attack
    public float timeBetweenAttacks = 1f; // adjust this value to set the time between melee attacks
    
    bool alreadyAttacked;
    private NavMeshAgent meleeEnemy;
    public Transform player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        meleeEnemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        meleeEnemy.SetDestination(player.position);
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
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
