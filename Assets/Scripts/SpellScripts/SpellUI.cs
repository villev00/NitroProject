using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    [SerializeField] GameObject[] spellSlots;
    [SerializeField] public GameObject spellManager;
    [SerializeField] public PhotonView pv;
    Spell[] currentSpells= new Spell[3];
   
    [SerializeField] GameObject spellInfoPanel;
    [SerializeField] GameObject[] spellInfos;


    [SerializeField] GameObject[] ammoBackground; 

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

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!spellInfoPanel.activeInHierarchy)
                spellInfoPanel.SetActive(true);
            else spellInfoPanel.SetActive(false);
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
        ChangeInfoPanel(spells);
    }

    
    void ShowSpellCooldown(Spell spell, int index)
    {
        // Start the coroutine for this spell if its on cd
        if (spell.isSpellOnCooldown)
            StartCoroutine(Cooldown(spell, index));
        else { 
            spellSlots[index].transform.GetChild(0).GetComponent<Slider>().value = 0;
            spellSlots[index].transform.GetChild(1).gameObject.SetActive(false);

        }

    }
    IEnumerator Cooldown(Spell spell, int index)
    {
        spellSlots[index].transform.GetChild(1).gameObject.SetActive(true);
        while (spell.isSpellOnCooldown)
        {
            spellSlots[index].transform.GetChild(0).GetComponent<Slider>().value = spell.cooldownRemaining / spell.spellCooldown;
            spellSlots[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = spell.cooldownRemaining.ToString();
            yield return new WaitForSeconds(1f);
        }
        spellSlots[index].transform.GetChild(0).GetComponent<Slider>().value = 0;
        spellSlots[index].transform.GetChild(1).gameObject.SetActive(false);
    }
    void ChangeInfoPanel(Spell[] spells)
    {
        if (spells[0].spellElement == Element.Fire)
        {
            spellInfoPanel.transform.GetChild(0).gameObject.SetActive(true);
            spellInfoPanel.transform.GetChild(1).gameObject.SetActive(false);
            spellInfoPanel.transform.GetChild(2).gameObject.SetActive(false);
            ammoBackground[0].gameObject.SetActive(true);
            ammoBackground[1].gameObject.SetActive(false);
            ammoBackground[2].gameObject.SetActive(false);
        }
        else if (spells[0].spellElement == Element.Aether)
        {
            spellInfoPanel.transform.GetChild(0).gameObject.SetActive(false);
            spellInfoPanel.transform.GetChild(1).gameObject.SetActive(true);
            spellInfoPanel.transform.GetChild(2).gameObject.SetActive(false);
            ammoBackground[0].gameObject.SetActive(false);
            ammoBackground[1].gameObject.SetActive(true);
            ammoBackground[2].gameObject.SetActive(false);
        }
        else if (spells[0].spellElement == Element.Lightning)
        {
            spellInfoPanel.transform.GetChild(0).gameObject.SetActive(false);
            spellInfoPanel.transform.GetChild(1).gameObject.SetActive(false);
            spellInfoPanel.transform.GetChild(2).gameObject.SetActive(true);
            ammoBackground[0].gameObject.SetActive(false);
            ammoBackground[1].gameObject.SetActive(false);
            ammoBackground[2].gameObject.SetActive(true);
        }
        for (int i = 0; i < spells.Length; i++)
        {
            spellInfos[i].GetComponent<Image>().sprite = spells[i].spellSprite;
            spellInfos[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = spells[i].spellName;
            spellInfos[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = spells[i].spellInfo;

        }

    }
 
}
