
using Photon.Pun;
using UnityEngine;

//Flame barrier is a protective spell that absorbs damage.
//Absorbable damage amount is 100. The spell lasts for 5 seconds. 
//The mana cost is 15 and the cooldown is 45 seconds.
public class FlameBarrier : MonoBehaviour
{
    PhotonView pv;

    [SerializeField] Spell spell;
    [SerializeField] int barrierHealth;
    [SerializeField] AudioClip spellClip;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
       
    }
    void Start()
    {
        if (!pv.IsMine) return;
        AudioManager.PlaySound(spellClip, false);
        //Let player know flamebarrier is up and set shield amount
        transform.root.GetComponent<PlayerLogic>().flameBarrier = gameObject;
        transform.root.GetComponent<PlayerLogic>().SetShieldValue(barrierHealth);

        //Move objects location to players location (from spawnpoint)
        transform.position = new Vector3(transform.root.position.x, transform.root.position.y, transform.root.position.z);
        Invoke("DestroySpell", spell.spellDuration);
    }
    void DestroySpell()
    {
        transform.root.GetComponent<PlayerLogic>().SetShieldValue(0);
        pv.RPC("RPC_DestroySpell", RpcTarget.All);
    }
    [PunRPC]
    void RPC_DestroySpell()
    {
        Destroy(gameObject);
    }
  
}
