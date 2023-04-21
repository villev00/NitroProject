using Photon.Pun;
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

 
    public float heavySwingRange;
    public int heavySwingDmg;
    public float timeBetweenAttacks; 

    public float magmaPoolDuration;
    public int magmaPoolDamage;

    public float homingDeathSpeed;
    public int homingDeathDmg;
    public float homingDeathLifetime;
    public float homingDeathSeekRadius;

    public float SwitchPlayerTime;
    int attackChoice;
    float delayBetweenAttacks;

    bool heavySwingUsed;
    bool magmaPoolUsed;
    bool homingDeathUsed;
   
    public bool playerInAttackRange;
    int playerIndex;
    Animator anim;
    private void Awake()
    {      
        bossEnemy = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;
        attackChoice = Random.Range(0, 3);
        SwitchPlayerTime = Random.Range(5f, 12f);
        delayBetweenAttacks = Random.Range(4f, 8f);
        InvokeRepeating("SwitchPlayer", 0f, SwitchPlayerTime);

        heavySwingRange = 2f;
        heavySwingDmg = 30;
        timeBetweenAttacks = 2f;

        magmaPoolDuration = 10f;
        magmaPoolDamage = 10;
            
        homingDeathSpeed = 5f;
        homingDeathDmg = 20;
        homingDeathLifetime = 5f;
        homingDeathSeekRadius = 5f;

        heavySwingUsed = false;
        magmaPoolUsed = false;
        homingDeathUsed = false;
    }

    private void Update()
    {        
        if (bossEnemy.enabled == false) return;            
        StartCoroutine(AttackPlayer());
        playerInAttackRange = Physics.CheckSphere(transform.position, heavySwingRange, Player);
        if (playerInAttackRange == true)
        {
            anim.SetBool("isRunning", false);
            HeavySwing();
        }
    }
    

    private IEnumerator AttackPlayer()
    {             

        switch (attackChoice)
        {
            case 0:
                if (!magmaPoolUsed)
                {
                    MagmaPool();
                    magmaPoolUsed = true;
                    yield return new WaitForSeconds(delayBetweenAttacks);
                    magmaPoolUsed = false;
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
        anim.SetBool("isRunning", true);
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
                anim.SetTrigger("meleeAttack");
                // apply damage to the player
                player.GetComponent<PlayerLogic>().TakeDamage(heavySwingDmg);
                //Debug.Log("HeavySwing");
            }

            heavySwingUsed = true;
            Invoke(nameof(ResetHeavySwing), timeBetweenAttacks);
        }

    }
    private void MagmaPool()
    {
        anim.SetTrigger("spellAttack");
        //Debug.Log("Magma Pool");
        GameObject magmaPool = Instantiate(magmaPoolPrefab, player.position, Quaternion.identity);
        Destroy(magmaPool, magmaPoolDuration);
    }
    private void HomingDeath()
    {
        //Debug.Log("Homing Death");
        anim.SetTrigger("spellAttack");
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

    private void SwitchPlayer()
    {
        delayBetweenAttacks = Random.Range(4f, 8f);
        attackChoice = Random.Range(0, 3);
        SwitchPlayerTime = Random.Range(5f, 12f);
        if (player == GameObject.FindGameObjectsWithTag("Player")[0].transform)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[1].transform;
        }
        else
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }              
    }
}
