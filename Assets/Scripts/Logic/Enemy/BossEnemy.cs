using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    public NavMeshAgent bossEnemy;
    BossHealth bossHealth;
    public Transform player;
    public Transform staff;
    public LayerMask Player;
    public float heavySwingRange;
    public int heavySwingDmg;
    public float timeBetweenAttacks;   
    public float SwitchPlayerTime;
    public float delayBetweenAttacks;
    public bool playerInAttackRange;
    public bool heavySwingInUse;
    public bool magmaPoolInUse;
    public bool homingDeathInUse;
    public bool isAttacking;
    private float lastAttackedAt;
    int playerIndex;
    Animator anim;
   
    // Attack pattern variables
    int currentAttackIndex = 0;
    List<Attack> attackPattern = new List<Attack>()
    {
        new Attack(AttackType.HeavySwing, 2f),
        new Attack(AttackType.MagmaPool, 4f),
        new Attack(AttackType.HeavySwing, 6f),
        new Attack(AttackType.HomingDeath, 8f),
        new Attack(AttackType.HeavySwing, 11f),
        new Attack(AttackType.MagmaPool, 13f),
        new Attack(AttackType.HeavySwing, 14f),
        new Attack(AttackType.HomingDeath, 15f),
    };

    private void Awake()
    {
        bossEnemy = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        bossHealth = GetComponent<BossHealth>();      
    }

    private void Start()
    {
        playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;
        SwitchPlayerTime = Random.Range(5f, 12f);        
        InvokeRepeating("SwitchPlayer", 0f, SwitchPlayerTime);

        heavySwingRange = 4f;
        heavySwingDmg = 30;
        timeBetweenAttacks = 2f;
      
        lastAttackedAt = -9999f;
        isAttacking = false;
        delayBetweenAttacks = attackPattern[0].delayBetweenAttacks;        
    }

    private void Update()
    {
        if (bossEnemy.enabled == false) return;
        if (bossHealth.isDead) bossEnemy.SetDestination(transform.position);

        if (!isAttacking)
        {
            if(Time.time>lastAttackedAt + delayBetweenAttacks)
            {
                PerformAttack(attackPattern[currentAttackIndex].attackType);
                lastAttackedAt = Time.time;
            }
        }
   
        playerInAttackRange = Physics.CheckSphere(transform.position, heavySwingRange, Player);
        if (playerInAttackRange)
        {
            if (!isAttacking)
            {
                if (Time.time > lastAttackedAt + timeBetweenAttacks)
                {                                      
                    HeavySwing();
                    lastAttackedAt = Time.time;
                }                              
            }
        }
        

        if (attackPattern[currentAttackIndex].attackType == AttackType.HeavySwing) heavySwingInUse = true; else heavySwingInUse = false;

        if (attackPattern[currentAttackIndex].attackType == AttackType.MagmaPool) magmaPoolInUse = true; else magmaPoolInUse = false;

        if (attackPattern[currentAttackIndex].attackType == AttackType.HomingDeath) homingDeathInUse = true; else homingDeathInUse = false;

    }


    private void PerformAttack(AttackType attackType)
    {      
        bossEnemy.isStopped = true;
        switch (attackType)
        {
            case AttackType.HeavySwing:
                HeavySwing();
                break;

            case AttackType.MagmaPool:
                MagmaPool();
                break;

            case AttackType.HomingDeath:
                HomingDeath();
                break;
        }       
        bossEnemy.isStopped = false;
        currentAttackIndex++;
        if (currentAttackIndex >= attackPattern.Count)
        {
            currentAttackIndex = 0;
        }
        delayBetweenAttacks = attackPattern[currentAttackIndex].delayBetweenAttacks;
    }
    private void HeavySwing()
    {
        isAttacking = true;
        if (!playerInAttackRange)
        {
            Debug.Log("Chasing");
            bossEnemy.SetDestination(player.position);
            anim.SetBool("isRunning", true);
        }
        
        // check if the player is within range for a melee attack
        if (Vector3.Distance(transform.position, player.position) <= heavySwingRange)
        {
            bossEnemy.isStopped = true;
            transform.LookAt(player);
            anim.SetTrigger("meleeAttack");
            // apply damage to the player
            player.GetComponent<PlayerLogic>().TakeDamage(heavySwingDmg);
            Debug.Log("HeavySwing");
        }          
        
        isAttacking = false;
    }

    private void MagmaPool()
    {
        isAttacking = true;
        anim.SetTrigger("spellAttack");
        Debug.Log("Magma Pool");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "LavaPool"), player.position, Quaternion.identity);
        }
        isAttacking = false;
    }

    private void HomingDeath()
    {
        isAttacking = true;
        Debug.Log("Homing Death");
        anim.SetTrigger("spellAttack");
        // Create a homing death projectile at the boss's position
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject homingDeath = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "HomingDeath"), staff.position, Quaternion.identity);
            homingDeath.GetComponent<HomingDeath>().player = player;
        }                  
        isAttacking = false;
    }

    private void SwitchPlayer()
    {       
        SwitchPlayerTime = Random.Range(5f, 12f);
        Debug.Log("Changing target");
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

public enum AttackType
{
    HeavySwing,
    MagmaPool,
    HomingDeath
}

public struct Attack
{
    public AttackType attackType;
    public float delayBetweenAttacks;

    
    
public Attack(AttackType attackType, float delayBetweenAttacks)
    {
        this.attackType = attackType;
        this.delayBetweenAttacks = delayBetweenAttacks;
    }
   
}