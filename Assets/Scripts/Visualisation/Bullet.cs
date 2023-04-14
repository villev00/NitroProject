using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    public Element element;
    public int damage;
    private ShootingLogic sLogic;
    PhotonView pv;
    [SerializeField]

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        damage = 25;
        //damage = sLogic.GetDamage();
        if(pv.IsMine)
            Invoke("DestroySpell",5);
    }
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!pv.IsMine) return;

        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            damage *= 2;
            Debug.Log("Headshot");
            collision.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(damage, 0);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bodyshot");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, 0);
        }
        
        DestroySpell();
    }


    void DestroySpell()
    {
        pv.RPC("RPC_DestroySpell", RpcTarget.All);
    }
    [PunRPC]
    void RPC_DestroySpell()
    {
        Destroy(gameObject);
    }
}
