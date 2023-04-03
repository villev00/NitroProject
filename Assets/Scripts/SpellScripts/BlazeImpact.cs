using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BlazeImpact : MonoBehaviour
{
    [SerializeField]
    Spell spell; //get damage details from spell object
    Vector3 target;
   
    [SerializeField]
    SphereCollider explosionCollider, hitCollider;
    [SerializeField]
    GameObject explosionEffect;

    [SerializeField]
    float spellMovementSpeed;
    [SerializeField]
    int knockbackStrength;


    bool firstHit;
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
            Debug.Log("Hit target "+collision.gameObject.name);
           // other.GetComponent<Enemy>().TakeDamage(spell.spellDamage) tms
            //enable trigger collider for explosion damage which has bigger radius
            explosionCollider.enabled = true;
            firstHit = true;
            StartCoroutine(DestroySpell());
        }
        //add force to explosion knockback after initial hit
        if (firstHit)
        {
            if(collision.gameObject.GetComponent<Rigidbody>()!=null)
                collision.gameObject.GetComponent<Rigidbody>().AddForce(0, knockbackStrength, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //damage enemies within explosion radius
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Explosion hit: " + other.name);
           // other.GetComponent<Enemy>().TakeDamage(spell.spellAreaDamage); tms
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
        yield return new WaitForSeconds(1f);
      
        Destroy(gameObject);
    }
}
