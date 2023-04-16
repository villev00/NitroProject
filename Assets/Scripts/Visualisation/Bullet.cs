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
    AudioClip projectileLaunch;
    [SerializeField]
    AudioClip headshotAudio;
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
        if (pv.IsMine)
        {
            AudioManager.PlaySound(projectileLaunch, false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!pv.IsMine) return;

        // Enemy hit
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            damage *= 2;
            collision.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(damage, element);
            AudioManager.PlaySound(headshotAudio, false);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, element);
        }

        // Boss hit
        else if (collision.gameObject.CompareTag("BossHead"))
        {
            damage *= 2;
            collision.gameObject.GetComponentInParent<BossHealth>().TakeDamage(damage, element);
            AudioManager.PlaySound(headshotAudio, false);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            try
            {
                collision.gameObject.GetComponent<BossHealth>().TakeDamage(damage, element);
            }
            catch
            {
                collision.gameObject.GetComponentInParent<BossHealth>().TakeDamage(damage, element);
            }
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
