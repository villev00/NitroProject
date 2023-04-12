using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;



//Fire torrent is a frontal cone type of spell that shoots fire in front
//of the caster dealing area of effect damage in a short range.
//The spell lasts for 5 seconds and needs to be channeled. The
//cast can be canceled early. Fire torrent deals 10 damage per
//second to all enemies in range. The mana cost is 15 and the
//cooldown is 20 seconds.
public class FireTorrent : MonoBehaviour
{
    PhotonView pv;
    [SerializeField] Spell spell;
   
    List<GameObject> enemies = new List<GameObject>();
   
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (!pv.IsMine) return;

        InvokeRepeating("Damage", 0, 1);
        Invoke("DestroySpell", spell.spellDuration);
    }
    private void Update()
    {
        if (!pv.IsMine) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f,1))
        {
            transform.LookAt(hit.point);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;
        if (other.CompareTag("Enemy"))
        {
            if (!enemies.Contains(other.gameObject))
            {
                enemies.Add(other.gameObject);
                Debug.Log("In damage zone: "+other.name); }
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (!pv.IsMine) return;
        if (other.CompareTag("Enemy"))
        {
            if (enemies.Contains(other.gameObject))
            {
                enemies.Remove(other.gameObject);
                Debug.Log("Enemy left damage zone: " + other.name);
            }
        }

    }
    void Damage()
    {
        foreach(GameObject enemy in enemies)
        {
            if (enemy != null)
                enemy.GetComponent<EnemyHealth>().TakeDamage(spell.spellDamage);  //NULLCHECK 
        }                                                                           //trap ei tuhoudu eikä chains     
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
