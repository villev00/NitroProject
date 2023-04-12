using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Element element;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            Debug.Log("Headshot");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(50);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bodyshot");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(25);
        }
        Destroy(this.gameObject);
    }

}
