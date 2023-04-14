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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        meleeEnemy = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        meleeRange = 2f;
        meleeDamage = 10;
        timeBetweenAttacks = 1.2f;
        alreadyAttacked = false;
    }

    void Update()
    {
        if (meleeEnemy.enabled == false) return;

        if (enemyHealth.isStunned == true)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isIdle", true);
            meleeEnemy.SetDestination(transform.position);
            //anim.enabled = false;
            lightningFX.gameObject.SetActive(true); // enable the Lightning FX component
        }
        else
        {          
            anim.enabled = true;          
            lightningFX.gameObject.SetActive(false); // disable the Lightning FX component
                                                     //Check for attack range        
            playerInAttackRange = Physics.CheckSphere(transform.position, meleeRange, Player);
            if (playerInAttackRange)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isAttacking", true);
                StartAttack();
            }
            else
            {
                anim.SetBool("isIdle", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isRunning", true);
                meleeEnemy.SetDestination(player.position);
            }
        }       
    }

    void StartAttack()
    {                                                    
        AttackPlayer();                                                           
    }

    private void AttackPlayer()
    {

        meleeEnemy.SetDestination(transform.position);
        transform.LookAt(player);
        
        if (!alreadyAttacked)
        {

            // check if the player is within range for a melee attack
            if (Vector3.Distance(transform.position, player.position) <= meleeRange +1)
            {                
                // apply damage to the player
                Debug.Log("Attacking Player");
                player.GetComponent<PlayerLogic>().TakeDamage(meleeDamage);              
            }            
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
