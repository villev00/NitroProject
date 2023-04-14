using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    public NavMeshAgent bossEnemy;
    public GameObject magmaPoolPrefab;
    public GameObject homingDeathPrefab;

    public Transform player;
   
    public Transform staff;

    public LayerMask Player;

 
    public float heavySwingRange = 2f;
    public int heavySwingDmg = 30;
    public float timeBetweenAttacks = 2f; 

    public float magmaPoolDuration = 10f;
    public int magmaPoolDamage = 10;

    public float homingDeathSpeed = 5f;
    public int homingDeathDmg = 20;
    public float homingDeathLifetime = 5f;
    public float homingDeathSeekRadius = 5f;

    bool heavySwingUsed;
    bool magmaPoolUsed;
    bool homingDeathUsed;
   
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossEnemy = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (bossEnemy.enabled == false) return;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (playerInSightRange) StartCoroutine(AttackPlayer());

        if (playerInAttackRange && playerInSightRange) HeavySwing();
    }
    

    private IEnumerator AttackPlayer()
    {
        // Generate a random number between 0 and 2
        int attackChoice = Random.Range(0, 3);

        // Set the delay between attacks to a random value between 4 and 8 seconds
        float delayBetweenAttacks = Random.Range(8f, 12f);

        switch (attackChoice)
        {
            case 0:
                if (!magmaPoolUsed)
                {
                   // MagmaPool();
                   // magmaPoolUsed = true;
                   // yield return new WaitForSeconds(delayBetweenAttacks);
                   // magmaPoolUsed = false;
                }
                break;

            case 1:
                if (!homingDeathUsed)
                {
                    HomingDeath();
                    homingDeathUsed = true;
                    yield return new WaitForSeconds(delayBetweenAttacks);
                    homingDeathUsed = false;
                }
                break;

            case 2:
                if (!heavySwingUsed)
                {
                    if (!playerInAttackRange)
                    {                        
                        ChasePlayer();
                    }
                    else
                    {                       
                        HeavySwing();
                    }                   
                    yield return new WaitForSeconds(delayBetweenAttacks);
                }
                break;
        }
    }

    private void ChasePlayer()
    {       
        bossEnemy.SetDestination(player.position);
    }

    private void HeavySwing()
    {
        bossEnemy.SetDestination(transform.position);

        transform.LookAt(player);

        if (!heavySwingUsed)
        {
            // check if the player is within range for a melee attack
            if (Vector3.Distance(transform.position, player.position) <= heavySwingRange)
            {
                // apply damage to the player
                player.GetComponent<PlayerLogic>().TakeDamage(heavySwingDmg);
                Debug.Log("HeavySwing");
            }

            heavySwingUsed = true;
            Invoke(nameof(ResetHeavySwing), timeBetweenAttacks);
        }

    }
    private void MagmaPool()
    {
        Debug.Log("Magma Pool");
        GameObject magmaPool = Instantiate(magmaPoolPrefab, player.position, Quaternion.identity);
        Destroy(magmaPool, magmaPoolDuration);
    }
    private void HomingDeath()
    {
        Debug.Log("Homing Death");
 
        // Create a homing death projectile at the boss's position
        GameObject homingDeath = Instantiate(homingDeathPrefab, staff.position, Quaternion.identity);

        // Set the homing target to the player's position
        homingDeath.GetComponent<HomingDeath>().SetTarget(new Vector3 (player.position.x, player.position.y + 1, player.position.z));

        // Destroy the homing death projectile after 10 seconds
        Destroy(homingDeath, 10f);

    }
    private void ResetHeavySwing()
    {
        heavySwingUsed = false;
    }
    private void ResetMagmaPool()
    {
        magmaPoolUsed = false;
    }
    private void ResetHomingDeath()
    {
        homingDeathUsed = false;
    }

}
