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
    public ParticleSystem lightningFX;
    EnemyHealth enemyHealth;
    bool isStunned;

    private void Awake()
    {
        rangedEnemy = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
        if (rangedEnemy.enabled == false) return;

        if (!enemyHealth.isStunned)
        {
            rangedEnemy.SetDestination(player.position);
            anim.enabled = true;
            anim.SetBool("isRunning", true);
            lightningFX.gameObject.SetActive(false); // disable the Lightning FX component
        }
        else
        {
            rangedEnemy.SetDestination(transform.position);
            anim.enabled = false;
            lightningFX.gameObject.SetActive(true); // enable the Lightning FX component
        }

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) StartAttack();
    }

    private void ChasePlayer()
    {
        rangedEnemy.SetDestination(player.position);
        if (!anim.GetBool("isRunning")) anim.SetBool("isRunning", true);
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
        //stopp
        rangedEnemy.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, gun.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);

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

