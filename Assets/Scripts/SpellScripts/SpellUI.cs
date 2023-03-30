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
    GameObject spellManager;

    private void Update()
    {
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
        for (int i = 0; i < spells.Length; i++)
        {
            spellSlots[i].GetComponent<Image>().sprite = spells[i].spellSprite;
            spellSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
            if (spells[i].spellElement == Element.Fire)
            {
                int buttonId = i;
                spellSlots[buttonId].GetComponent<Button>().onClick.AddListener(delegate { spellManager.GetComponent<FireSpells>().UseFireSpell(spells[buttonId]); });
            }
            else if (spells[i].spellElement == Element.Lightning)
            {
                int buttonId = i;
                spellSlots[buttonId].GetComponent<Button>().onClick.AddListener(delegate { spellManager.GetComponent<LightningSpells>().UseLightningSpell(spells[buttonId]); });
            }
            else if (spells[i].spellElement == Element.Aether)
            {
                int buttonId = i;
                spellSlots[buttonId].GetComponent<Button>().onClick.AddListener(delegate { spellManager.GetComponent<AetherSpells>().UseAetherSpell(spells[buttonId]); });
            }

        }
    }
}
