using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

//Magnetic grasp is a spell used on two enemies that will be pulled
//together. Enemies can also be pulled against a wall. Direct hit will
//deal 5 damage and the pull deals 10 additional damage. The 
//mana cost is 15 and the cooldown is 10 seconds.

public class MagneticGrasp : MonoBehaviour
{
    PhotonView pv;

    [SerializeField] Spell spell;
    GameObject target;
    bool magnetEnabled;
    public bool isEnemy;
    bool areBothEnemies;

    Rigidbody parentRb;
    NavMeshAgent parentNav;

    //feature: jos vihu kuolee graspin aikana niin kyseinen spelli j‰‰ n‰kyviin toiselle pelaajalle
    [SerializeField] AudioClip spellClip;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
        transform.parent = null;
        if (!pv.IsMine) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, 1, QueryTriggerInteraction.Ignore))
        {
            transform.parent = hit.transform;
            transform.position = hit.point;
        }
        if (transform.parent.GetComponent<EnemyHealth>() != null)
        {
            isEnemy = true;
            transform.parent.GetComponent<EnemyHealth>().TakeDamage(spell.spellDamage, spell.spellElement);
            parentNav = transform.root.GetComponent<NavMeshAgent>();
            parentRb = transform.root.GetComponent<Rigidbody>();
        }
        Invoke("DestroySpell", 5);
        AudioManager.PlaySound(spellClip, false);
    }
    private void Update()
    {
        if (!pv.IsMine) return;
        if (magnetEnabled) MagnetEffect();    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;
        if (other.GetComponentInChildren<MagneticGrasp>() != null)
        {
            target = other.gameObject;
            magnetEnabled = true;
            if (other.GetComponentInChildren<MagneticGrasp>().isEnemy && isEnemy)
            { 
               areBothEnemies = true;
            }
            if (isEnemy)
            {
                parentNav.enabled = false;
                parentRb.isKinematic = false;
                parentRb.useGravity = false;
                transform.parent.GetComponent<EnemyHealth>().TakeDamage(spell.spellDamage, spell.spellElement);

            }
           
        }

    }
    void MagnetEffect()
    {
        //if both targets are enemies, pull them towards each other
        if (areBothEnemies && target!=null)
        {
            transform.root.position = Vector3.MoveTowards(transform.root.position, target.transform.position, 5 * Time.deltaTime);
           
        }
        else if (!isEnemy && target != null && target.GetComponentInChildren<MagneticGrasp>()!=null) //this magnet is on environment but other one is on enemy, pull enemy towards this object
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, transform.position, 5 * Time.deltaTime);
           
        }
        else if (target == null)
        {
            DestroySpell();
        }
    }
    public void DestroySpell()
    {
        
        pv.RPC("RPC_DestroySpell", RpcTarget.All);
        if (isEnemy)
        {
            parentNav.enabled = true;
            parentRb.isKinematic = true;
            parentRb.useGravity = true;
        }
        if (!magnetEnabled)
        {
            var photonViews = UnityEngine.Object.FindObjectsOfType<PhotonView>();
            foreach (var view in photonViews)
            {
             if (view.gameObject.CompareTag("Player") == true && view.IsMine)
             {
                var playerPrefabObject = view.gameObject;
                playerPrefabObject.GetComponent<Spells>().magnetCounter = 0;
             }
            }
           
        }
    }
    [PunRPC]
    void RPC_DestroySpell()
    {
        Destroy(gameObject);
    }
}
