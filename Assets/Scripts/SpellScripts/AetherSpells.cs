using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherSpells : MonoBehaviour
{
    public SpellUI spellUI;
    [SerializeField]
    Spell[] aetherSpells;

    Spell magneticGrasp, aethericLeap, blackHole;
    bool magneticGraspOnCooldown, aethericLeapOnCooldown, blackHoleOnCooldown;
    
    void Start()
    {
        spellUI = GameObject.Find("UIManager").GetComponent<SpellUI>();
        SetupSpells();
    }
    void SetupSpells()
    {
        for (int i = 0; i < aetherSpells.Length; i++)
        {
            if (aetherSpells[i].spellName == "Magnetic Grasp")
            {
                magneticGrasp = aetherSpells[i];
            }
            if (aetherSpells[i].spellName == "Aetheric Leap")
            {
                aethericLeap = aetherSpells[i];
            }
            if (aetherSpells[i].spellName == "Black Hole")
            {
                blackHole = aetherSpells[i];
            }
        }
    }
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            spellUI.ChangeSpellSet(aetherSpells);
        }
    }

    public void UseAetherSpell(Spell spell)
    {
        if (spell.spellName == "Magnetic Grasp" && !magneticGraspOnCooldown)
        {
            Debug.Log("Magnetic grasp used");
            magneticGraspOnCooldown = true;
            StartCoroutine(MagneticGraspCooldown());
        }
        else if (spell.spellName == "Aetheric Leap" && !aethericLeapOnCooldown)
        {
            Debug.Log("Aetheric Leap used");
            aethericLeapOnCooldown = true;
            StartCoroutine(AethericLeapCooldown());
        }
        else if (spell.spellName == "Black Hole" && !blackHoleOnCooldown)
        {
            Debug.Log("Black Hole used");
            blackHoleOnCooldown = true;
            StartCoroutine(BlackHoleCooldown());
        }

    }


    #region SpellCooldowns

    IEnumerator MagneticGraspCooldown()
    {
        yield return new WaitForSeconds(magneticGrasp.spellCooldown);
        magneticGraspOnCooldown = false;
        Debug.Log("Magnetic Grasp ready");
    }
    IEnumerator AethericLeapCooldown()
    {
        yield return new WaitForSeconds(aethericLeap.spellCooldown);
        aethericLeapOnCooldown = false;
        Debug.Log("Aetheric Leap ready");
    }
    IEnumerator BlackHoleCooldown()
    {
        yield return new WaitForSeconds(blackHole.spellCooldown);
        blackHoleOnCooldown = false;
        Debug.Log("Black Hole ready");
    }
    #endregion
}
