using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public float meleeRange;
    public int meleeDamage;
    public float timeBetweenAttacks;
    public LayerMask Player;
    bool alreadyAttacked;
    private NavMeshAgent meleeEnemy;
    public Transform player;
    public ParticleSystem lightningFX;

    public bool playerInAttackRange;


    Animator anim;
    EnemyHealth enemyHealth;

    private void Awake()
    {
        meleeRange = 2f;
        meleeDamage = 10;
        timeBetweenAttacks = 1f;
        alreadyAttacked = false;      

        player = GameObject.FindGameObjectWithTag("Player").transform;
        meleeEnemy = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        if (meleeEnemy.enabled == false) return;

        if (enemyHealth.isStunned)
        {
            meleeEnemy.SetDestination(transform.position);
            anim.enabled = false;
            lightningFX.gameObject.SetActive(true); // enable the Lightning FX component
        }
        else
        {
            meleeEnemy.SetDestination(player.position);
            anim.enabled = true;          
            lightningFX.gameObject.SetActive(false); // disable the Lightning FX component
        }

        //Check for attack range        
        playerInAttackRange = Physics.CheckSphere(transform.position, meleeRange, Player);
        if (playerInAttackRange) StartAttack();
    }

    void StartAttack()
    {       
        if (anim.GetBool("isAttacking"))
        {
            anim.SetBool("isRunning", false);

            if (!enemyHealth.isStunned)
            {
               // anim.SetBool("isAttacking", true);
                AttackPlayer();
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
        yield return new WaitForSeconds(timeBetweenAttacks);

    }
}
