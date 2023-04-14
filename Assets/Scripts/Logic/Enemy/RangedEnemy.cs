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

    public int rangedDamage;
    public float attackRange;
    public bool playerInAttackRange;
    Animator anim;
    public ParticleSystem lightningFX;
    EnemyHealth enemyHealth;

    private void Awake()
    {
        rangedEnemy = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        rangedDamage = 15;
        attackRange = 10f;
        timeBetweenAttacks = 1.2f;
        alreadyAttacked = false;
    }

    private void Update()
    {
        if (rangedEnemy.enabled == false) return;

        if (enemyHealth.isStunned == true)
        {
            anim.SetBool("isIdle", true);
            rangedEnemy.SetDestination(transform.position);
           // anim.enabled = false;
            lightningFX.gameObject.SetActive(true); // enable the Lightning FX component
        }
        else
        {
            anim.enabled = true;
            lightningFX.gameObject.SetActive(false); // disable the Lightning FX component
                                                     //Check for attack range        
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);
            if (playerInAttackRange)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isAttacking", true);
                StartAttack();
            }
            else
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isAttacking", false);
                rangedEnemy.SetDestination(player.position);
            }
        }
    }

    void StartAttack()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {              
        rangedEnemy.SetDestination(transform.position);
        
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, gun.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            player.GetComponent<PlayerLogic>().TakeDamage(rangedDamage);
            alreadyAttacked = true;
            StartCoroutine(ResetAttack());
        }

    }

    private IEnumerator ResetAttack()
    {        
        yield return new WaitForSeconds(timeBetweenAttacks);
        alreadyAttacked = false;
        yield return new WaitForSeconds(timeBetweenAttacks);
    }
}

