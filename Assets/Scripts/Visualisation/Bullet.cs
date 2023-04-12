using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    public Element element;
    PhotonView pv;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        if(pv.IsMine)
            Invoke("DestroySpell",5);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!pv.IsMine) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(25);
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
