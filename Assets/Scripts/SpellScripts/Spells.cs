using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    SpellUI spellUI;

    [SerializeField]
    Spell[] fireSpells;
    [SerializeField]
    Spell[] lightningSpells;
    [SerializeField]
    Spell[] aetherSpells;

    [SerializeField]
    GameObject spellSpawn;

    int magnetCounter;

    void Start()
    {
        spellUI = GameObject.Find("UIManager").GetComponent<SpellUI>();
        spellUI.ChangeSpellSet(fireSpells); //Show fire spells first in the UI
        SetupSpells();
    }
    //Reset spell cooldowns when game starts
    void SetupSpells()
    {
        for (int i = 0; i < fireSpells.Length; i++)
        {
            fireSpells[i].isSpellOnCooldown = false;
            lightningSpells[i].isSpellOnCooldown = false;
            aetherSpells[i].isSpellOnCooldown = false;
        }

    }
    private void Update()
    {
        //Keybinds for different spell sets
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spellUI.ChangeSpellSet(fireSpells);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            spellUI.ChangeSpellSet(lightningSpells);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            spellUI.ChangeSpellSet(aetherSpells);
        }
    }

    public void UseSpell(Spell spell)
    {
        if (!spell.isSpellOnCooldown) //If spell is not on cooldown, use that spell and set it on cooldown
        {
            if (spell.spellName != "Magnetic Grasp") 
            {
                spell.isSpellOnCooldown = true;
                StartCoroutine(spell.SpellCooldown());
            }
            else //Magnetic grasp needs to be cast twice for it to work
            {
                magnetCounter++;
                if(magnetCounter==2)
                {
                    magnetCounter = 0;
                    spell.isSpellOnCooldown = true;
                    StartCoroutine(spell.SpellCooldown());
                }
            }
            Debug.Log(spell.spellName + " used");
            if (spell.spellPrefab != null)
            {
                //Instantiate spellprefab at the end of magical weapon,and set its parent
                GameObject go = Instantiate(spell.spellPrefab, spellSpawn.transform.position, Quaternion.identity);
                go.transform.parent = spellSpawn.transform;
            }
                
        }
       
    }

}
