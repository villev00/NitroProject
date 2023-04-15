using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    [SerializeField] GameObject [] spellSlots;
    [SerializeField] Slider[] cooldownSliders;
    [SerializeField] List<float> allCooldowns = new List<float>();
    List<int> cooldownId = new List<int>();
   
    [SerializeField] public GameObject spellManager;

    [SerializeField] public PhotonView pv;

    Spell[] currentSpells= new Spell[3];

    private void Start()
    {
        for(int i=0; i < 9; i++)
        {
            allCooldowns.Add(0);
        }
    }
    private void Update()
    {
        if (!pv.IsMine) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spellSlots[0].GetComponent<Button>().onClick.Invoke();
            ShowSpellCooldown(currentSpells[0], 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            spellSlots[1].GetComponent<Button>().onClick.Invoke();
            ShowSpellCooldown(currentSpells[1],1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spellSlots[2].GetComponent<Button>().onClick.Invoke();
            ShowSpellCooldown(currentSpells[2],2);
        }
    }

    //jos samassa spellslotissa on kaksi spelliä cd, niin timer visuaali kusee
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
            ShowSpellCooldown(currentSpells[i], i);
        }
    }
    void ShowSpellCooldown(Spell spell, int sliderId)
    {
        int cdId = 0;
        if (spell.spellElement == Element.Fire) //0 1 2
            cdId = sliderId;
        else if (spell.spellElement == Element.Aether) //3 4 5     
            cdId = sliderId + 3;
        else if (spell.spellElement == Element.Lightning) //6 7 8
            cdId = sliderId + 6;

        // Start the coroutine for this spell slot if cooldown is 0 and no coroutine is already running for it
        if (allCooldowns[cdId] == 0 && spell.isSpellOnCooldown)
        {
            allCooldowns[cdId] = spell.spellCooldown;
            cooldownId.Add(cdId);
            StartCoroutine(DoShowSpellCooldown(spell, sliderId, cdId));
        }
        // If a coroutine is already running for this spell slot, don't start another one
        else if (allCooldowns[cdId] > 0 && cooldownId.Contains(cdId))
        {
            Debug.Log("Coroutine already running for spell " + spell.spellName);
            cooldownSliders[sliderId].value = allCooldowns[cdId] / spell.spellCooldown;
        }                                                  
        else
        {
            Debug.Log("Spell not on cd" +spell.spellName);
            //Spell is not on cooldown, remove slider
            cooldownSliders[sliderId].value = 0;
        }
    }

    IEnumerator DoShowSpellCooldown(Spell spell, int sliderId, int cooldownId)
    {
        Debug.Log("Started spell coroutine");
        float sliderValueChange = 1f / spell.spellCooldown;
        cooldownSliders[sliderId].value = allCooldowns[cooldownId];
        while (spell.isSpellOnCooldown)
        {
         yield return new WaitForSeconds(1f);
         cooldownSliders[sliderId].value -= sliderValueChange;
         allCooldowns[cooldownId] -= 1f;
        }
        // Clear the cooldown value
        allCooldowns[cooldownId] = 0;
    }
}
