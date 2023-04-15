using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    [SerializeField] GameObject [] spellSlots;
    [SerializeField] Slider[] cooldownSliders;

    [SerializeField] public GameObject spellManager;

    [SerializeField] public PhotonView pv;

    Spell[] currentSpells= new Spell[3];

    private void Update()
    {
        if (!pv.IsMine) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spellSlots[0].GetComponent<Button>().onClick.Invoke();
            StartCoroutine(ShowSpellCooldown(currentSpells[0], 0));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            spellSlots[1].GetComponent<Button>().onClick.Invoke();
            StartCoroutine(ShowSpellCooldown(currentSpells[1],1));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spellSlots[2].GetComponent<Button>().onClick.Invoke();
            StartCoroutine(ShowSpellCooldown(currentSpells[2],2));
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
            currentSpells[i] = spells[i];
          //  StartCoroutine(ShowSpellCooldown(currentSpells[i], i));
        }
    }

    IEnumerator ShowSpellCooldown(Spell spell, int sliderId)
    {
        float sliderValueChange = 1f/spell.spellCooldown;
        float remainingCd = spell.spellCooldown;
   
        while (spell.isSpellOnCooldown)
        {
            yield return new WaitForSeconds(1f);
            cooldownSliders[sliderId].value -= sliderValueChange;
            Debug.Log(spell + " cd 1 sec lesss ");
            remainingCd -= 1f;
            Debug.Log("remaining: " + remainingCd);
        }
        
    }
}
