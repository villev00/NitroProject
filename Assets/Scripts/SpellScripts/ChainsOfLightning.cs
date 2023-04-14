using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
//Chains of lightning is an area of effect type of skill. The spell will
//hit all enemies in close range of the target. Enemies will get
//stunned and take damage.  Stun duration is 5 seconds and 
//damage amount is 20. Mana cost is 15 and the cooldown is 20
//seconds.

public class ChainsOfLightning : MonoBehaviour
{
    PhotonView pv;
    Vector3 target;
    [SerializeField] Spell spell;
    [SerializeField] float spellMovementSpeed;
    [SerializeField] GameObject explosionEffect;
    bool hitTriggered, isDestroyed;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
        transform.parent = null;
        if (!pv.IsMine) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, 1))
        {
            target = hit.point;
        }

        Invoke("DestroySpell", spell.spellDuration);
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (!pv.IsMine) return;
        transform.position = Vector3.MoveTowards(transform.position, target, spellMovementSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;
        //damage enemies within explosion radius 
        if (other.CompareTag("Wall")|| other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            hitTriggered = true;
            Debug.Log("chains hit: " + other.name);
            if (hitTriggered && !isDestroyed) StartCoroutine(DestroySpell());

            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyHealth>().TakeDamage(spell.spellAreaDamage, Element.Lightning);
                //  other.GetComponent<EnemyHealth>().GetStunned(5);
            }
            else if (other.gameObject.CompareTag("Boss"))
            {
                other.GetComponent<BossHealth>().TakeDamage(spell.spellAreaDamage);
                //  other.GetComponent<EnemyHealth>().GetStunned(5);
            }
        }
       


    }
    IEnumerator DestroySpell()
    {
        Debug.Log("chains destroy inc");
        isDestroyed = true;
        explosionEffect.SetActive(true);
        GetComponent<SphereCollider>().radius = 3;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.8f);
        pv.RPC("RPC_DestroySpell", RpcTarget.All);
    }
    [PunRPC]
    void RPC_DestroySpell()
    {
        Destroy(gameObject);
    }
}
