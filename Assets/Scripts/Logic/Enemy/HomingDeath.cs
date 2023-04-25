using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingDeath : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 200f;
    public float lifeTime = 3f;
    public int damage = 20;
    PhotonView pv;
    public Transform player;
    private Vector3 target;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        Invoke("DestroyHomingDeath", lifeTime);
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
        target = new Vector3(player.position.x, player.position.y + 1, player.position.z);
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
            DestroyHomingDeath();
        }      
    }

    void DestroyHomingDeath()
    {
        if (pv.IsMine) pv.RPC("RPC_DestroyMagmaPool", RpcTarget.All);
    }
    void RPC_DestroyHomingDeath()
    {
        Destroy(gameObject);
    }
}
