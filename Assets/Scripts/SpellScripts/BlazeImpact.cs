using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BlazeImpact : MonoBehaviour
{
    [SerializeField]
    Spell spell;
    Vector3 target;
    [SerializeField]
    SphereCollider explosionCollider, hitCollider;

    [SerializeField]
    GameObject explosionEffect;

    [SerializeField]
    float spellMovementSpeed;

    [SerializeField]
    int knockbackStrength;
    bool damagedEnemy;
    private void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f))
        {
            target = hit.point;
        }
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime *spellMovementSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Damage first enemy hit with blaze impact,
        //making collider bigger later for knockback effect and dont want to do first hit damage again
        if (collision.gameObject.CompareTag("Enemy") && !damagedEnemy)
        {
            Debug.Log("Hit enemy "+collision.gameObject.name);
           // other.GetComponent<Enemy>().TakeDamage(spell.spellDamage) tms
            //enable trigger collider for explosion damage which has bigger radius
            explosionCollider.enabled = true;
            damagedEnemy = true;
            StartCoroutine(DestroySpell());
        }
        //add force to explosion knockback
        if (damagedEnemy)
        {
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
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
