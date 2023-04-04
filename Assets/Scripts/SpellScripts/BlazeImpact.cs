using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


//Blaze impact is a spell that travels in a straight line and hits the
//first enemy or wall on its way. Enemies and the player will be
//knocked back within its range. Blaze impact deals 50 dmg to the
//enemy hit directly and 25 to enemies nearby. The mana cost 15
//and the cooldown is 10 seconds.
public class BlazeImpact : MonoBehaviour
{
    [SerializeField]
    Spell spell; //get damage details from spell object
    Vector3 target;
   
    //Colliders for spell, one for direct hit and one for explosion radius
    [SerializeField]
    SphereCollider explosionCollider, hitCollider;
    [SerializeField]
    GameObject explosionEffect;

    [SerializeField]
    float spellMovementSpeed;
    [SerializeField]
    int knockbackStrength;


    bool firstHit;

    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();
    private void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f,1))
        {
            target = hit.point;
        }
        transform.parent = null;
       
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target,spellMovementSpeed*Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Damage first enemy hit with blaze impact,
        //or explode when hitting wall
        if (collision.gameObject.CompareTag("Enemy") && !firstHit || collision.gameObject.CompareTag("Wall") && !firstHit)
        {
            //enable trigger collider for explosion damage which has bigger radius
            Debug.Log("Hit target "+collision.gameObject.name);
            explosionCollider.enabled = true;
            firstHit = true;
            StartCoroutine(DestroySpell());
            if(collision.gameObject.GetComponent<EnemyHealth>()!=null)
                 collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(spell.spellDamage);
           
           
        }
        //add force to explosion knockback after initial hit
        if (firstHit)
        {
            if (collision.gameObject.GetComponent<Rigidbody>()!=null)
                collision.gameObject.GetComponent<Rigidbody>().AddForce(knockbackStrength, knockbackStrength, knockbackStrength);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //damage enemies within explosion radius
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!enemies.Contains(other.gameObject))
            {
                enemies.Add(other.gameObject);
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                Debug.Log("Explosion hit: " + other.name);
                other.GetComponent<EnemyHealth>().TakeDamage(spell.spellAreaDamage);
            }
               
           
        }
       
    }

    IEnumerator DestroySpell()
    {
       
        //increase spell visual effect and hitcollider size to knock nearby enemies
        transform.localScale = transform.localScale * 5;
        hitCollider.radius = explosionCollider.radius;
        explosionEffect.SetActive(true);
        
        yield return new WaitForSeconds(0.2f);
        transform.localScale = transform.localScale / 5;
        hitCollider.radius = 1;
        explosionCollider.enabled = false;
        yield return new WaitForSeconds(1f);
    
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Rigidbody>().isKinematic = true;
                enemy.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
              
        }
        
        Destroy(gameObject);
    }
}
