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
    SphereCollider explosionCollider;

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
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime *5);
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
        transform.localScale = transform.localScale * 7;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
