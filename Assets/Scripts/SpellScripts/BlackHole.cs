using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Black hole is a spell that pulls enemies within a small range
//together but doesn’t do any damage. The mana cost is 15 and
//the cooldown is 15 seconds.
public class BlackHole : MonoBehaviour
{
    PhotonView pv;
    [SerializeField] Spell spell;
    [SerializeField] float pullSpeed;

    List<GameObject> enemies = new List<GameObject>();
    bool triggerEffect;

    [SerializeField] AudioClip spellClip;
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
            transform.position = new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z);
            transform.eulerAngles = new Vector3(-90, 0, 0);
            transform.parent = null;
        }
        Invoke("DestroySpell", spell.spellDuration);
        AudioManager.PlaySound(spellClip, true);
    }
    private void Update()
    {
        if (!pv.IsMine) return;
        //After black hole is entered, start pulling enemies inside
        if (triggerEffect)
        {
            foreach(GameObject enemy in enemies)
            {
                if(enemy!=null)
                    enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, transform.position, pullSpeed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;
        //Add enemies in trigger zone to list, so their location can be accessed during pull effect
        if (other.CompareTag("Enemy") && !enemies.Contains(other.gameObject))
        {
           enemies.Add(other.gameObject);
           triggerEffect = true; 
        }

    }
    public void DestroySpell()
    {
        pv.RPC("RPC_DestroySpell", RpcTarget.All);
    }
    [PunRPC]
    void RPC_DestroySpell()
    {
        Destroy(gameObject);
    }
}
