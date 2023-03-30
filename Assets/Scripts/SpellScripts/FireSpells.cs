using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class FireSpells : MonoBehaviour
{

    public SpellUI spellUI;
    [SerializeField]
    Spell[] fireSpells;

    Spell blazeImpact, fireTorrent, flameBarrier;
    bool blazeImpactOnCooldown, fireTorrentOnCooldown, flameBarrierOnCooldown;

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
            if (fireSpells[i].spellName == "Blaze Impact")
            {
                blazeImpact = fireSpells[i];
            }
            if (fireSpells[i].spellName == "Fire Torrent")
            {
                fireTorrent= fireSpells[i];
            }
            if (fireSpells[i].spellName == "Flame Barrier")
            {
                flameBarrier = fireSpells[i];
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spellUI.ChangeSpellSet(fireSpells);  
        }
    }

    public void UseFireSpell(Spell spell)
    {
        if(spell.spellName=="Blaze Impact" && !blazeImpactOnCooldown)
        {
            Debug.Log("Blaze impact used");
            blazeImpactOnCooldown = true;
            Instantiate(blazeImpact.spellPrefab, Camera.main.transform);
            StartCoroutine(BlazeImpactCooldown());
        }else if(spell.spellName == "Fire Torrent" && !fireTorrentOnCooldown)
        {
            Debug.Log("Fire Torrent used");
            Instantiate(fireTorrent.spellPrefab, Camera.main.transform);
            fireTorrentOnCooldown = true;
            StartCoroutine(FireTorrentCooldown());
        }else if (spell.spellName == "Flame Barrier" && !flameBarrierOnCooldown)
        {
            Debug.Log("Flame Barrier used");
            flameBarrierOnCooldown = true;
            GameObject barrier = Instantiate(flameBarrier.spellPrefab, Camera.main.transform);
            StartCoroutine(FlameBarrierCooldown(barrier));
        }     
    }

    #region SpellCooldowns
   
    IEnumerator BlazeImpactCooldown()
    {
        yield return new WaitForSeconds(blazeImpact.spellCooldown);
        blazeImpactOnCooldown = false;
        Debug.Log("Blaze impact ready");
    }
    IEnumerator FireTorrentCooldown()
    {
        yield return new WaitForSeconds(fireTorrent.spellCooldown);
        fireTorrentOnCooldown = false;
        Debug.Log("Torrent ready");
    }
    IEnumerator FlameBarrierCooldown(GameObject barrier)
    {
        yield return new WaitForSeconds(flameBarrier.spellDuration);
        Destroy(barrier);
        Debug.Log("Barrier ended");
        yield return new WaitForSeconds(flameBarrier.spellCooldown-flameBarrier.spellDuration);
        flameBarrierOnCooldown = false;
        Debug.Log("Barrier ready");
    }
    #endregion
}
