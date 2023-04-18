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
    AudioClip[] elementLaunch;
    [SerializeField]
    AudioClip[] elementHit;
    [SerializeField]
    GameObject[] visualElement;
    Transform spellSpawn;
    GameObject sparkle;
    //[SerializeField]
    //AudioClip projectileLaunch; // temporary
    //[SerializeField]
    //AudioClip headshotAudio;    // temporary
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        damage = 25;
        //damage = sLogic.GetDamage();
        if (pv.IsMine)
        {
            Invoke("DestroySpell",5);
            //spellSpawn = GetComponent<ShootingController>().GetSpellSpawn();
        }
    }
    private void Start()
    {
        if (pv.IsMine)
        {
            switch (element)
            {
                case Element.Fire:
                    visualElement[0].SetActive(true);
                    AudioManager.PlaySound(elementLaunch[0], false);
                    break;
                case Element.Aether:
                    visualElement[1].SetActive(true);
                    AudioManager.PlaySound(elementLaunch[1], false);
                    break;
                case Element.Lightning:
                    visualElement[2].SetActive(true);
                    AudioManager.PlaySound(elementLaunch[2], false);
                    break;
                default:
                    break;
            }
            //AudioManager.PlaySound(projectileLaunch, false);
        }
    }

    private void Update()
    {
        //StaffVisualGlow();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!pv.IsMine) return;

        // Enemy hit
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            damage *= 2;
            collision.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(damage, element);
            //AudioManager.PlaySound(headshotAudio, false);
            HeadshotSound();
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
            //AudioManager.PlaySound(headshotAudio, false);
            HeadshotSound();
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

    void HeadshotSound()
    {
        switch (element)
        {
            case Element.Fire:
                AudioManager.PlaySound(elementHit[0], false);
                break;
            case Element.Aether:
                AudioManager.PlaySound(elementHit[1], false);
                break;
            case Element.Lightning:
                AudioManager.PlaySound(elementHit[2], false);
                break;
            default:
                break;
        }
        
        sparkle.transform.position = spellSpawn.transform.position;
    }

    void StaffVisualGlow()
    {
        switch (element)
        {
            case Element.Fire:
                sparkle = visualElement[0].transform.GetChild(2).gameObject;
                break;
            case Element.Aether:
                sparkle = visualElement[1].transform.GetChild(3).gameObject;
                break;
            case Element.Lightning:
                sparkle = visualElement[2].transform.GetChild(5).gameObject;
                break;
            default:
                break;
        }
        sparkle.transform.position = spellSpawn.position;
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
