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
            target = hit.transform.gameObject;
            transform.position = hit.point;
        }
        transform.parent = target.transform;
        if (transform.parent.GetComponent<EnemyHealth>() != null)
        {
            isEnemy = true;
            transform.parent.GetComponent<EnemyHealth>().TakeDamage(spell.spellDamage);
        }
        Invoke("DestroySpell", 5);
       
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
            Debug.Log("other magnetic " + other.name);
            target = other.gameObject;
            magnetEnabled = true;
            if (other.GetComponentInChildren<MagneticGrasp>().isEnemy)
            {
                other.GetComponent<NavMeshAgent>().enabled = false;
                other.GetComponent<Rigidbody>().isKinematic = false;
                target.GetComponent<EnemyHealth>().TakeDamage(spell.spellAreaDamage);
                if (isEnemy) areBothEnemies = true;

            }
            if (isEnemy)
            {
                transform.root.GetComponent<NavMeshAgent>().enabled = false;
                transform.root.GetComponent<Rigidbody>().isKinematic = false;
               
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
        else if (isEnemy && target != null && target.GetComponentInChildren<MagneticGrasp>()!=null) //this magnet is on enemy but other one is on environment, pull this object towards environment
        {
            transform.root.position = Vector3.MoveTowards(transform.root.position, target.GetComponentInChildren<MagneticGrasp>().gameObject.transform.position, 5 * Time.deltaTime);
           
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
            transform.root.GetComponent<NavMeshAgent>().enabled = true;
            transform.root.GetComponent<Rigidbody>().isKinematic = true;
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
