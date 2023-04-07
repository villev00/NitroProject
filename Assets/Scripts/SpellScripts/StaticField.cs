using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
//Static field sets a trap that activates after a short delay. When
//an enemy steps on it, the trap triggers and nearby enemies get
//stunned and take damage. Stun time is 5 seconds and damage 
//is 25. Mana cost is 15 and the cooldown is 10 seconds.
public class StaticField : MonoBehaviour
{
    PhotonView pv;
    [SerializeField] Spell spell;
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
            transform.position = hit.point;
        }

        Invoke("DestroySpell", spell.spellDuration);
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
