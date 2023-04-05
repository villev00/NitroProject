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
    [SerializeField]
    Spell spell;
    GameObject target;
    bool magnetEnabled;
    public bool enemy;
    bool bothEnemies;
    void Start()
    {
        target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, 1, QueryTriggerInteraction.Ignore))
        {
            target = hit.transform.gameObject;
            transform.position = hit.point;
        }
        transform.parent = target.transform;
        if (transform.parent.GetComponent<EnemyHealth>() != null)
        {
            enemy = true;
            transform.parent.GetComponent<EnemyHealth>().TakeDamage(spell.spellDamage);
        }

        Destroy(gameObject, 5);
    }
    private void Update()
    {
        if (magnetEnabled) 
        {
            MagnetEffect();
        }
            
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<MagneticGrasp>() != null)
        {
            Debug.Log("other magnetic " + other.name);
            target = other.gameObject;
            magnetEnabled = true;
            if (other.GetComponentInChildren<MagneticGrasp>().enemy)
            {
                other.GetComponent<NavMeshAgent>().enabled = false;
                other.GetComponent<Rigidbody>().isKinematic = false;
                target.GetComponent<EnemyHealth>().TakeDamage(spell.spellAreaDamage);
                if (enemy)
                {
                    transform.root.GetComponent<NavMeshAgent>().enabled = false;
                    transform.root.GetComponent<Rigidbody>().isKinematic = false;
                    bothEnemies = true;
                }

            }
        }

    }
    void MagnetEffect()
    {
        //if both targets are enemies, pull them towards each other
        if (bothEnemies)
        {
            transform.root.position = Vector3.MoveTowards(transform.root.position, target.transform.position, 2 * Time.deltaTime);
            target.transform.position = Vector3.MoveTowards(target.transform.position, transform.root.position, 2 * Time.deltaTime);
            if (Vector3.Distance(transform.root.position, target.transform.position) < 0.1f)
            {
                transform.root.GetComponent<NavMeshAgent>().enabled = true;
                target.GetComponent<NavMeshAgent>().enabled = true;
                Destroy(target.transform.GetComponentInChildren<MagneticGrasp>().gameObject);
                Destroy(gameObject);
            }
        }
        else if (enemy) //this magnet is on enemy but other one is on environment, pull this object towards environment
        {
            transform.root.position = Vector3.MoveTowards(transform.root.position, target.GetComponentInChildren<MagneticGrasp>().gameObject.transform.position, 2 * Time.deltaTime);
            if (Vector3.Distance(transform.root.position, target.transform.position) < 0.1f)
            {
                transform.root.GetComponent<NavMeshAgent>().enabled = true;
                Destroy(target.transform.GetComponentInChildren<MagneticGrasp>().gameObject);
                Destroy(gameObject);
            }
        }
        else if (!enemy)//this magnet is on environment but other one is on enemy, pull that enemy towards this object
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, transform.position, 2 * Time.deltaTime);
            if (Vector3.Distance(transform.root.position, target.transform.position) < 0.1f)
            {
                target.GetComponent<NavMeshAgent>().enabled = true;
                Destroy(target.transform.GetComponentInChildren<MagneticGrasp>().gameObject);
                Destroy(gameObject);
            }
        }
}
}
