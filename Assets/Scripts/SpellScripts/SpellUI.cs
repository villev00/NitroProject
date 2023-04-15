using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    [SerializeField] GameObject[] spellSlots;
    public GameObject[] fireSlots;
    public GameObject[] aetherSlots;
    public GameObject[] lightningSlots;

    public GameObject[] activeSlots = new GameObject[3];
    [SerializeField] public GameObject spellManager;

    [SerializeField] public PhotonView pv;

    Spell[] currentSpells= new Spell[3];
    private void Start()
    {
       // activeSlots = fireSlots;
    }
    private void Update()
    {
        if (!pv.IsMine) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spellSlots[0].GetComponent<Button>().onClick.Invoke();
           // activeSlots[0].GetComponent<Button>().onClick.Invoke();
             ShowSpellCooldown(currentSpells[0], 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            spellSlots[1].GetComponent<Button>().onClick.Invoke();
           // activeSlots[1].GetComponent<Button>().onClick.Invoke();
            ShowSpellCooldown(currentSpells[1],1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spellSlots[2].GetComponent<Button>().onClick.Invoke();
         //   activeSlots[2].GetComponent<Button>().onClick.Invoke();
            ShowSpellCooldown(currentSpells[2],2);
        }
    }
    public void ChangeSpellSet(Spell[] spells)
    {
      
        if (!pv.IsMine) return;
        StopAllCoroutines();
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
    //jos samassa spellslotissa on kaksi spelliä cd, niin timer visuaali kusee
    //public void ChangeSpellSet(Spell[] spells)
    //{
    //    if (!pv.IsMine) return;
    //    for (int i = 0; i < activeSlots.Length;i++)
    //    {
    //        if (activeSlots[i].gameObject!=null)
    //        activeSlots[i].gameObject.SetActive(false);
           
    //    }
    //    StopAllCoroutines();
    //    if (spells[0].spellElement==Element.Fire)activeSlots = fireSlots;
    //    else if (spells[0].spellElement == Element.Aether) activeSlots = aetherSlots;
    //    else if (spells[0].spellElement == Element.Lightning) activeSlots = lightningSlots;

    //    for (int i = 0; i < spells.Length; i++)
    //    {
    //        activeSlots[i].gameObject.SetActive(true);
    //        activeSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
    //        int index = i;
    //        activeSlots[i].GetComponent<Button>().onClick.AddListener(delegate { spellManager.GetComponent<Spells>().UseSpell(spells[index]); });
    //        currentSpells[i] = spells[i];
    //        ShowSpellCooldown(currentSpells[i], i);
           
    //    }
    //}
    void ShowSpellCooldown(Spell spell, int index)
    {
        // Start the coroutine for this spell if its on cd
        if (spell.isSpellOnCooldown)
            StartCoroutine(Cooldown(spell, index));
        //StartCoroutine(ShowCooldown(spell, index));
       // else activeSlots[index].transform.GetChild(0).GetComponent<Slider>().value = 0;
        else spellSlots[index].transform.GetChild(0).GetComponent<Slider>().value = 0;

    }
    IEnumerator Cooldown(Spell spell, int index)
    {
        while (spell.isSpellOnCooldown)
        {
            Debug.Log("cd remaining: " + spell.cooldownRemaining);
          //  activeSlots[index].transform.GetChild(0).GetComponent<Slider>().value = spell.cooldownRemaining / spell.spellCooldown;
            spellSlots[index].transform.GetChild(0).GetComponent<Slider>().value = spell.cooldownRemaining / spell.spellCooldown;
            yield return new WaitForSeconds(1f);
        }
        spellSlots[index].transform.GetChild(0).GetComponent<Slider>().value = 0;
    }
}
