using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingDeath : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 200f;
    public float lifeTime = 3f;
    public int damage = 20;
    PlayerLogic pLogic;
    private Vector3 target;
    void Awake()
    {
        pLogic = GetComponent<PlayerLogic>();
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name + " was hit by " + gameObject.name);
            collision.gameObject.GetComponent<PlayerLogic>().TakeDamage(damage);
            Destroy(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }      
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
