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
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        attackRange = 10f;
        timeBetweenAttacks = 3f;
        alreadyAttacked = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (rangedEnemy.enabled == false || enemyHealth.health <= 0) return;

        if (enemyHealth.isStunned == true)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
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
               
               StartAttack();
            }
            else
            {
                anim.SetBool("isIdle", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isRunning", true);
                rangedEnemy.SetDestination(player.position);
            }
        }
    }

    void StartAttack()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isAttacking", true);
    }

    private void AttackPlayer()
    {
       // Debug.Log("Start attack");
        rangedEnemy.SetDestination(transform.position);
        
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            GameObject projectileObj = Instantiate(projectile, gun.position, Quaternion.identity);
            Rigidbody rb = projectileObj.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 16f, ForceMode.Impulse);
            alreadyAttacked = true;
            StartCoroutine(ResetAttack());
            Destroy(projectileObj, 5f);
        }

    }

    private IEnumerator ResetAttack()
    {        
        yield return new WaitForSeconds(timeBetweenAttacks);
        alreadyAttacked = false;      
    }
}

