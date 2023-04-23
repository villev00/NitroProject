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
    BossHealth bossHealth;
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
    public float delayBetweenAttacks;
    public bool playerInAttackRange;
    public bool heavySwingInUse;
    public bool magmaPoolInUse;
    public bool homingDeathInUse;
    int playerIndex;
    Animator anim;

    // Attack pattern variables
    int currentAttackIndex = 0;
    List<Attack> attackPattern = new List<Attack>()
    {
        new Attack(AttackType.HeavySwing, 5f),
        new Attack(AttackType.MagmaPool, 8f),
        new Attack(AttackType.HomingDeath, 11f),
        new Attack(AttackType.HeavySwing, 14f),
        new Attack(AttackType.MagmaPool, 17f),
        new Attack(AttackType.HomingDeath, 20f),
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

        heavySwingRange = 2f;
        heavySwingDmg = 30;
        timeBetweenAttacks = 2f;

        magmaPoolDuration = 10f;
        magmaPoolDamage = 10;

        homingDeathSpeed = 5f;
        homingDeathDmg = 20;
        homingDeathLifetime = 5f;
        homingDeathSeekRadius = 5f;

        delayBetweenAttacks = attackPattern[0].delayBetweenAttacks;      
    }

    private void Update()
    {
        if (bossEnemy.enabled == false) return;
        if (bossHealth.isDead) bossEnemy.SetDestination(transform.position);
        if (Time.time > attackPattern[currentAttackIndex].delayBetweenAttacks)
        {
            StartCoroutine(PerformAttack(attackPattern[currentAttackIndex].attackType));           
        }

        playerInAttackRange = Physics.CheckSphere(transform.position, heavySwingRange, Player);
        if (playerInAttackRange)
        {
            anim.SetBool("isRunning", false);
            bossEnemy.SetDestination(transform.position);
            HeavySwing();
        }

        if (attackPattern[currentAttackIndex].attackType == AttackType.HeavySwing) heavySwingInUse = true; else heavySwingInUse = false;

        if (attackPattern[currentAttackIndex].attackType == AttackType.MagmaPool) magmaPoolInUse = true; else magmaPoolInUse = false;

        if (attackPattern[currentAttackIndex].attackType == AttackType.HomingDeath) homingDeathInUse = true; else homingDeathInUse = false;

    }


    private IEnumerator PerformAttack(AttackType attackType)
    {      
        bossEnemy.isStopped = true;
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
        yield return new WaitForSeconds(delayBetweenAttacks);
        bossEnemy.isStopped = false;
        currentAttackIndex++;
        if (currentAttackIndex >= attackPattern.Count)
        {
            currentAttackIndex = 0;
        }
        delayBetweenAttacks = attackPattern[currentAttackIndex].delayBetweenAttacks;
    }
    private IEnumerator HeavySwing()
    {
        if (!playerInAttackRange)
        {
            bossEnemy.SetDestination(player.position);
            anim.SetBool("isRunning", true);
        }
        else
        {
            bossEnemy.isStopped = true;
            transform.LookAt(player);

            // check if the player is within range for a melee attack
            if (Vector3.Distance(transform.position, player.position) <= heavySwingRange)
            {
                anim.SetTrigger("meleeAttack");
                // apply damage to the player
                player.GetComponent<PlayerLogic>().TakeDamage(heavySwingDmg);
                Debug.Log("HeavySwing");
            }
            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }

    private IEnumerator MagmaPool()
    {
        yield return new WaitForSeconds(delayBetweenAttacks);
        anim.SetTrigger("spellAttack");
        Debug.Log("Magma Pool");
        GameObject magmaPool = Instantiate(magmaPoolPrefab, player.position, Quaternion.identity);
        Destroy(magmaPool, magmaPoolDuration);
        yield return new WaitForSeconds(delayBetweenAttacks);
    }
    private IEnumerator HomingDeath()
    {
        yield return new WaitForSeconds(delayBetweenAttacks);
        Debug.Log("Homing Death");
        anim.SetTrigger("spellAttack");
        // Create a homing death projectile at the boss's position
        GameObject homingDeath = Instantiate(homingDeathPrefab, staff.position, Quaternion.identity);

        // Set the homing target to the player's position
        homingDeath.GetComponent<HomingDeath>().SetTarget(new Vector3(player.position.x, player.position.y + 1, player.position.z));

        // Destroy the homing death projectile after 10 seconds
        Destroy(homingDeath, 10f);
        yield return new WaitForSeconds(delayBetweenAttacks);

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