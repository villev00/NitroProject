using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    void Start()
    {
        
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet") return;

        Debug.Log("Monsteriin osuttu");
        int damage = 50; //collision.gameObject.GetComponent<Bullet>().damage;
        if (this.gameObject.CompareTag("EnemyHead"))
        {
            damage *= 2;
            Debug.Log("Headshot");
        }
        else
        {
            Debug.Log("Bodyshot");
        }
        enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
    }
    */
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Bullet") return;

        Debug.Log("Monsteriin osuttu");
        int damage = 50; //collision.gameObject.GetComponent<Bullet>().damage;
        Debug.Log(this.gameObject.tag);
        if (this.gameObject.CompareTag("EnemyHead"))
        {
            damage *= 2;
            //Debug.Log("Headshot");
        }
        else
        {
            //Debug.Log("Bodyshot");
        }
        enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
    }
}
