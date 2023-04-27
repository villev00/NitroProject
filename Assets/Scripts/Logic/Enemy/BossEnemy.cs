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
    private bool isChasing;
    int playerIndex;
    Animator anim;
   
    // Attack pattern variables
    int currentAttackIndex = 0;
    List<Attack> attackPattern = new List<Attack>()
    {
        new Attack(AttackType.HeavySwing, 2f),
        new Attack(AttackType.MagmaPool, 2f),
        new Attack(AttackType.HeavySwing, 2f),
        new Attack(AttackType.HomingDeath, 2f),
        new Attack(AttackType.HeavySwing, 2f),
        new Attack(AttackType.MagmaPool, 2f),
        new Attack(AttackType.HeavySwing, 2f),
        new Attack(AttackType.HomingDeath, 2f),
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
        isChasing = true;
        delayBetweenAttacks = attackPattern[0].delayBetweenAttacks;        
    }

    private void Update()
    {
        if (bossEnemy.enabled == false) return;
        if (bossHealth.isDead) bossEnemy.isStopped = true;
        playerInAttackRange = Physics.CheckSphere(transform.position, heavySwingRange, Player);

        if (isChasing && !playerInAttackRange)
        {
            bossEnemy.SetDestination(player.position);           
            anim.SetBool("isRunning", true);           
        }
        else
        {
            bossEnemy.isStopped = true;
            anim.SetBool("isRunning", false);
        }

        if (!isAttacking)
        {
            if(Time.time>lastAttackedAt + delayBetweenAttacks)
            {
                PerformAttack(attackPattern[currentAttackIndex].attackType);
                lastAttackedAt = Time.time;
            }
        }
   
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
        isChasing = false;
        switch (attackType)
        {
            case AttackType.HeavySwing:
                StartCoroutine(HeavySwing());
                break;

            case AttackType.MagmaPool:
                StartCoroutine(MagmaPool());
                break;

            case AttackType.HomingDeath:
                StartCoroutine(HomingDeath());
                break;
        }       
        bossEnemy.isStopped = false;
        isChasing = true;
        currentAttackIndex++;
        if (currentAttackIndex >= attackPattern.Count)
        {
            currentAttackIndex = 0;
        }
        delayBetweenAttacks = attackPattern[currentAttackIndex].delayBetweenAttacks;
    }
    private IEnumerator HeavySwing()
    {
        isAttacking = true;
        
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
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    private IEnumerator MagmaPool()
    {
        isAttacking = true;
        bossEnemy.isStopped = true;
        anim.SetBool("isRunning", false);
        anim.SetTrigger("spellAttack");
        Debug.Log("Magma Pool");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "LavaPool"), player.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    private IEnumerator HomingDeath()
    {
        isAttacking = true;
        bossEnemy.isStopped = true;
        anim.SetBool("isRunning", false);
        Debug.Log("Homing Death");
        anim.SetTrigger("spellAttack");
        // Create a homing death projectile at the boss's position
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject homingDeath = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "HomingDeath"), staff.position, Quaternion.identity);
            homingDeath.GetComponent<HomingDeath>().player = player;
            homingDeath.GetComponent<HomingDeath>().SetTarget(player.gameObject);
        }
        yield return new WaitForSeconds(1f);
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