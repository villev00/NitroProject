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

    Transform spellSpawn;
    GameObject sparkle;
    [SerializeField]
    AudioClip projectileLaunch; 
    [SerializeField]
    AudioClip headshotAudio;    
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        damage = 25;
        //damage = sLogic.GetDamage();
        if (pv.IsMine)
        {
            Invoke("DestroySpell",5);
            //spellSpawn = GetComponent<ShootingController>().GetSpellSpawn();
           // spellSpawn = Camera.main.transform.GetChild(0).GetChild(0).transform;
        }
    }
    private void Start()
    {
        if (pv.IsMine) AudioManager.PlaySound(projectileLaunch,false);

    }

    private void Update()
    {
       // StaffVisualGlow();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!pv.IsMine) return;

        // Enemy hit
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            damage *= 2;
            collision.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(damage, element, transform.position, true);
            AudioManager.PlaySound(headshotAudio, false);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, element, transform.position, false);
        }

        // Boss hit
        else if (collision.gameObject.CompareTag("BossHead"))
        {
            damage *= 2;
            collision.gameObject.GetComponentInParent<BossHealth>().TakeDamage(damage, element, transform.position, true);
            AudioManager.PlaySound(headshotAudio, false);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            try
            {
                collision.gameObject.GetComponent<BossHealth>().TakeDamage(damage, element, transform.position, false);
            }
            catch
            {
                collision.gameObject.GetComponentInParent<BossHealth>().TakeDamage(damage, element,transform.position, false);
            }
        }
        DestroySpell();
    }


    // Does not work
    void StaffVisualGlow()
    {
        switch (element)
        {
            case Element.Fire:
                //sparkle = visualElement[0].transform.GetChild(2).gameObject;
                break;
            case Element.Aether:
                //sparkle = visualElement[1].transform.GetChild(3).gameObject;
                break;
            case Element.Lightning:
                //sparkle = visualElement[2].transform.GetChild(5).gameObject;
                break;
            default:
                break;
        }
        if (pv.IsMine)
        {
            sparkle.transform.position = Camera.main.transform.GetChild(0).GetChild(0).transform.position; //spellSpawn.transform.position;
        }
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
