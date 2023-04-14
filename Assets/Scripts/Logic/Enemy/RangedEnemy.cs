using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    public NavMeshAgent rangedEnemy;
    
    public Transform player;

    public LayerMask Player;

    public Transform gun;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

  
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    Animator anim;
    private void Awake()
    {
        rangedEnemy = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    private void Update()
    {
        if (rangedEnemy.enabled == false) return;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) StartAttack();
    }
    private void ChasePlayer()
    {
        rangedEnemy.SetDestination(player.position);
        if(!anim.GetBool("isRunning")) anim.SetBool("isRunning", true);
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
       //stopp
        rangedEnemy.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
           
            Rigidbody rb = Instantiate(projectile, gun.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);

            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        anim.SetBool("isAttacking", false);
       
    }
  
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

