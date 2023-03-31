using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public SpellUI spellUI;

    [SerializeField]
    Spell[] fireSpells;
    [SerializeField]
    Spell[] lightningSpells;
    [SerializeField]
    Spell[] aetherSpells;

    
    void Start()
    {
        spellUI = GameObject.Find("UIManager").GetComponent<SpellUI>();
        spellUI.ChangeSpellSet(fireSpells);

        SetupSpells();
    }

    void SetupSpells()
    {
        for (int i = 0; i < fireSpells.Length; i++)
        {
            fireSpells[i].isSpellOnCooldown = false;
        }
        for (int i = 0; i < lightningSpells.Length; i++)
        {
           lightningSpells[i].isSpellOnCooldown = false;
        }
        for (int i = 0; i < aetherSpells.Length; i++)
        {
            aetherSpells[i].isSpellOnCooldown = false;
        }

    }
    private void Update()
    {
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
        if (!spell.isSpellOnCooldown)
        {
            Debug.Log(spell.spellName + " used");
            spell.isSpellOnCooldown = true;
            if (spell.spellPrefab != null)
                Instantiate(spell.spellPrefab, Camera.main.transform);
        }
        StartCoroutine(spell.SpellCooldown());
    }

}
