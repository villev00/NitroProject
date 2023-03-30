using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class FireSpells : MonoBehaviour
{

    public SpellUI spellUI;
    [SerializeField]
    Spell[] fireSpells;

    Spell blazeImpact, fireTorrent, flameBarrier;
    bool blazeImpactUsed, fireTorrentUsed, flameBarrierUsed;

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
    #region BlazeImpact
    public void BlazeImpact()
    {
        if (blazeImpactUsed) return;
        Debug.Log("Blaze impact used");
        blazeImpactUsed = true;

        StartCoroutine(BlazeImpactCooldown());
    }

    IEnumerator BlazeImpactCooldown()
    {
        yield return new WaitForSeconds(blazeImpact.spellCooldown);
        blazeImpactUsed = false;
        Debug.Log("Blaze impact ready");
    }
    #endregion

    #region FireTorrent
    public void FireTorrent()
    {
        if (fireTorrentUsed) return;
        Debug.Log("Fire Torrent used");
        fireTorrentUsed = true;

        StartCoroutine(FireTorrentCooldown());
    }

    IEnumerator FireTorrentCooldown()
    {
        yield return new WaitForSeconds(fireTorrent.spellCooldown);
        fireTorrentUsed = false;
        Debug.Log("Torrent ready");
    }
    #endregion

    #region FlameBarrier
    public void FlameBarrier()
    {
        if (flameBarrierUsed) return;
        Debug.Log("Flame Barrier used");
        flameBarrierUsed = true;

        StartCoroutine(FlameBarrierCooldown());
    }

    IEnumerator FlameBarrierCooldown()
    {
        yield return new WaitForSeconds(flameBarrier.spellCooldown);
        flameBarrierUsed = false;
        Debug.Log("Barrier ready");
    }
    #endregion
}
