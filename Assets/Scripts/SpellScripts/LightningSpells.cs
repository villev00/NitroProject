using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LightningSpells : MonoBehaviour
{
    public SpellUI spellUI;
    [SerializeField]
    Spell[] lightningSpells;


    Spell staticField, tempestSurge, chainsOfLightning;
    bool staticFieldOnCooldown, tempestSurgeOnCooldown, chainsOfLightningOnCooldown;
    // Start is called before the first frame update
    void Start()
    {
        spellUI = GameObject.Find("UIManager").GetComponent<SpellUI>();
        SetupSpells();
    }
    void SetupSpells()
    {
        for (int i = 0; i < lightningSpells.Length; i++)
        {
            if (lightningSpells[i].spellName == "Static Field")
            {
                staticField = lightningSpells[i];
            }
            if (lightningSpells[i].spellName == "Tempest Surge")
            {
                tempestSurge = lightningSpells[i];
            }
            if (lightningSpells[i].spellName == "Chains of Lightning")
            {
                chainsOfLightning = lightningSpells[i];
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            spellUI.ChangeSpellSet(lightningSpells);
        }
    }
    public void UseLightningSpell(Spell spell)
    {
        if (spell.spellName == "Static Field" && !staticFieldOnCooldown)
        {
            Debug.Log("Static field used");
            staticFieldOnCooldown = true;
            StartCoroutine(StaticFieldCooldown());
         
        }
        else if (spell.spellName == "Tempest Surge" && !tempestSurgeOnCooldown)
        {
            Debug.Log("Tempest surge used");
            tempestSurgeOnCooldown = true;
            StartCoroutine(TempestSurgeCooldown());
        }
        else if (spell.spellName == "Chains of Lightning" && !chainsOfLightningOnCooldown)
        {
            Debug.Log("Chains of lightning used");
            chainsOfLightningOnCooldown = true;
            StartCoroutine(ChainsOfLightningCooldown());
        }

    }

    #region SpellCooldowns

    IEnumerator StaticFieldCooldown()
    {
        yield return new WaitForSeconds(staticField.spellCooldown);
        staticFieldOnCooldown = false;
        Debug.Log("Static field ready");
    }
    IEnumerator TempestSurgeCooldown()
    {
        yield return new WaitForSeconds(tempestSurge.spellCooldown);
        tempestSurgeOnCooldown = false;
        Debug.Log("Tempest surge ready");
    }
    IEnumerator ChainsOfLightningCooldown()
    {
        yield return new WaitForSeconds(chainsOfLightning.spellCooldown);
        chainsOfLightningOnCooldown = false;
        Debug.Log("Chains of lightning ready");
    }
    #endregion
}
