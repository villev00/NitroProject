using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Tempest surge gives the player a movement speed and attack
//speed buff for a short time. Basic attacks will also stun enemies
//for a short time. Movement speed is 20 % faster and attack 
//speed is 50 % faster. Buff spell lasts 10 seconds and the 
//cooldown is 20 seconds. Mana cost is 10.
public class TempestSurge : MonoBehaviour
{
    PhotonView pv;
    [SerializeField] Spell spell;
    [SerializeField] float speedModifier, attackSpeedModifier;
    float speed;
    float attackSpeed;
    [SerializeField] AudioClip spellClip;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
       
    }
    void Start()
    {
        if (!pv.IsMine) return;
        
        speed = transform.root.GetComponent<PlayerLogic>().GetSpeed();
        attackSpeed = transform.root.GetComponent<ShootingLogic>().GetRateOfFire();
        transform.root.GetComponent<ShootingLogic>().SetRateOfFire(attackSpeed * attackSpeedModifier);
        transform.root.GetComponent<PlayerLogic>().SetSpeed(speed * speedModifier);
        transform.root.GetComponent<ShootingController>().statChange();
        transform.root.GetComponent<PlayerControl>().statChange();
        AudioManager.PlaySound(spellClip, false);
        Invoke("DestroySpell", spell.spellDuration);
    }


    void DestroySpell()
    {
       transform.root.GetComponent<PlayerLogic>().SetSpeed(speed);
       transform.root.GetComponent<ShootingLogic>().SetRateOfFire(attackSpeed);
       transform.root.GetComponent<ShootingController>().statChange();
       transform.root.GetComponent<PlayerControl>().statChange();
        pv.RPC("RPC_DestroySpell", RpcTarget.All);
    }
    [PunRPC]
    void RPC_DestroySpell()
    {
        Destroy(gameObject);
    }
}
