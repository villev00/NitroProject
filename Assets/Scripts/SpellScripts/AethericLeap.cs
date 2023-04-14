using Photon.Pun;
using System.Collections;
using UnityEngine;



//Aetheric leap is a spell that works in a similar way like magnetic 
//grasp, but instead of pulling enemies you can pull yourself into a
//desired location. The mana cost is 5 and the cooldown is 5 seconds.
public class AethericLeap : MonoBehaviour
{
    Vector3 target;
    PhotonView pv;
    [SerializeField] AudioClip spellClip;
    //leap menee seinien läpi
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (!pv.IsMine) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f))
        {
            target = hit.point;
            transform.position = target;
            StartCoroutine(Teleport());
        }
        AudioManager.PlaySound(spellClip, true);
    }
 
    IEnumerator Teleport()
    {
        //After a short while teleport to target location,
        //Delay is for the player to see the visual effect
        yield return new WaitForSeconds(0.2f);
        //Disable character controller or it prevents teleporting
        transform.root.gameObject.GetComponent<CharacterController>().enabled = false;
        if (target.y > 3)
        {
            target = new Vector3(target.x, target.y - 0.8f, target.z);
        }
       
        transform.root.position = target;
        transform.root.gameObject.GetComponent<CharacterController>().enabled = true;
        pv.RPC("RPC_DestroySpell", RpcTarget.All);
    }

    [PunRPC]
    void RPC_DestroySpell()
    {
        Destroy(gameObject);
    }
}
