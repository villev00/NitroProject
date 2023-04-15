using Photon.Pun;
using System.IO;
using UnityEngine;

public class Spells : MonoBehaviour
{
    PhotonView pv;
    SpellUI spellUI;
    ShootingLogic slogic;

    [SerializeField] Spell[] fireSpells;
    [SerializeField] Spell[] lightningSpells;
    [SerializeField] Spell[] aetherSpells;

    [SerializeField] GameObject spellSpawn;
    public int magnetCounter;
    
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        spellUI = GameObject.Find("UIManager").GetComponent<SpellUI>();
        slogic = GetComponent<ShootingLogic>();
    }
    void Start()
    {
        if (!pv.IsMine) return;
        SetupSpells();
    }
    //Reset spell cooldowns when game starts
    void SetupSpells()
    {
        spellUI.ChangeSpellSet(fireSpells); //Show fire spells first in the UI
        for (int i = 0; i < fireSpells.Length; i++)
        {
            fireSpells[i].isSpellOnCooldown = false;
            lightningSpells[i].isSpellOnCooldown = false;
            aetherSpells[i].isSpellOnCooldown = false;
        }
       
    }
    private void Update()
    {
        if (!pv.IsMine) return;
        //Keybinds for different spell sets
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spellUI.ChangeSpellSet(fireSpells);
            slogic.SetElement(Element.Fire);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            spellUI.ChangeSpellSet(aetherSpells);
            slogic.SetElement(Element.Aether);
           
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            spellUI.ChangeSpellSet(lightningSpells);
            slogic.SetElement(Element.Lightning);
           
        }
       
    }
  


    public void UseSpell(Spell spell)
    {
        if (pv.IsMine)
        {
            //If spell is not on cooldown and theres enough mana, use that spell and set it on cooldown
            if (!spell.isSpellOnCooldown) 
              {
                if (spell.spellName != "Magnetic Grasp" && GetComponent<PlayerLogic>().GetMana() >= spell.spellManaCost)
                {
                    Debug.Log(spell.spellName + " used");

                    GameObject spellObj = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs/SpellPrefabs", spell.spellPrefab.name), spellSpawn.transform.position, Quaternion.identity);
                    spellObj.transform.SetParent(spellSpawn.transform); // set the parent immediately after instantiating the spell object

                    pv.RPC("RPC_SetParent", RpcTarget.Others, spellObj.GetPhotonView().ViewID, GetParentViewID(spellSpawn));
                    spell.isSpellOnCooldown = true;
                    StartCoroutine(spell.SpellCooldown());
                    spell.cooldownRemaining = spell.spellCooldown;
                    StartCoroutine(spell.CountSpellCooldown());
                    GetComponent<PlayerLogic>().LoseMana(spell.spellManaCost);
                }
                else if(spell.spellName == "Magnetic Grasp") //Cast magnetic grasp
                {
                    magnetCounter++;
                    //Magnetic grasp needs to be cast twice for it to work
                    if (magnetCounter != 2 && GetComponent<PlayerLogic>().GetMana() >= spell.spellManaCost)
                    {
                       
                        //Mana cost only on first grasp
                        GetComponent<PlayerLogic>().LoseMana(spell.spellManaCost);
                    }
                    else if(magnetCounter==2)
                    {
                        magnetCounter = 0;
                        spell.isSpellOnCooldown = true;
                        StartCoroutine(spell.SpellCooldown());
                    }
                    Debug.Log(spell.spellName + " used");

                    GameObject spellObj = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs/SpellPrefabs", spell.spellPrefab.name), spellSpawn.transform.position, Quaternion.identity);
                    spellObj.transform.SetParent(spellSpawn.transform); // set the parent immediately after instantiating the spell object

                    pv.RPC("RPC_SetParent", RpcTarget.Others, spellObj.GetPhotonView().ViewID, GetParentViewID(spellSpawn));
                }                         
            }
           
        }

    }
    int GetParentViewID(GameObject child)
    {
        Transform parent = child.transform.parent;
        while (parent != null)
        {
            PhotonView view = parent.GetComponent<PhotonView>();
            if (view != null)
            {
                return view.ViewID;
            }
            parent = parent.parent;
        }
        return -1;
    }

    [PunRPC]
    void RPC_SetParent(int spellObjID, int spellSpawnParentID)
    {
        GameObject spellObj = PhotonView.Find(spellObjID).gameObject;
        GameObject spellSpawnParent = PhotonView.Find(spellSpawnParentID).gameObject;
        GameObject spellSpawn = spellSpawnParent.transform.GetChild(0).GetChild(3).GetChild(0).gameObject;
        //GameObject spellSpawn = Camera.main.transform.GetChild(0).GetChild(0).gameObject; 
        spellObj.transform.SetParent(spellSpawn.transform);
    }
}
