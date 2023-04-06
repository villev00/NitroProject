using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    [SerializeField]
    GameObject [] spellSlots;

    [SerializeField]
    public GameObject spellManager;

    [SerializeField]
    public PhotonView pv;
   
    private void Update()
    {
        if (!pv.IsMine) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spellSlots[0].GetComponent<Button>().onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            spellSlots[1].GetComponent<Button>().onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spellSlots[2].GetComponent<Button>().onClick.Invoke();
        }
    }


    public void ChangeSpellSet(Spell[] spells)
    {
        if (!spellManager.GetComponent<PhotonView>().IsMine) return;
        for (int i = 0; i < spells.Length; i++)
        {
            spellSlots[i].GetComponent<Image>().sprite = spells[i].spellSprite;
            spellSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
            int index = i;
            spellSlots[index].GetComponent<Button>().onClick.AddListener(delegate { spellManager.GetComponent<Spells>().UseSpell(spells[index]); });
        }
    }
}
