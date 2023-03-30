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

    float blazeCounter, torrentCounter, barrierCounter;
    bool blazeUsed, torrentUsed, barrierUsed;

    void Start()
    {
        spellUI = GameObject.Find("UIManager").GetComponent<SpellUI>();
        spellUI.ChangeSpellSet(fireSpells);

        SetupValues();
    }

    void SetupValues()
    {
        for (int i = 0; i < fireSpells.Length; i++)
        {
            if (fireSpells[i].spellName == "Blaze Impact")
            {
                blazeCounter = fireSpells[i].spellCooldown;
            }
            if (fireSpells[i].spellName == "Fire Torrent")
            {
                torrentCounter = fireSpells[i].spellCooldown;
            }
            if (fireSpells[i].spellName == "Flame Barrier")
            {
                barrierCounter = fireSpells[i].spellCooldown;
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
        if (blazeUsed) return;
        Debug.Log("Blaze impact used");
        blazeUsed = true;

        StartCoroutine(BlazeCooldown());
    }

    IEnumerator BlazeCooldown()
    {
        yield return new WaitForSeconds(blazeCounter);
        blazeUsed = false;
        Debug.Log("Blaze impact ready");
    }
    #endregion

    #region FireTorrent
    public void FireTorrent()
    {
        if (torrentUsed) return;
        Debug.Log("Fire Torrent used");
        torrentUsed = true;

        StartCoroutine(TorrentCooldown());
    }

    IEnumerator TorrentCooldown()
    {
        yield return new WaitForSeconds(torrentCounter);
        torrentUsed = false;
        Debug.Log("Torrent ready");
    }
    #endregion

    #region FlameBarrier
    public void FlameBarrier()
    {
        if (barrierUsed) return;
        Debug.Log("Flame Barrier used");
        barrierUsed = true;

        StartCoroutine(BarrierCooldown());
    }

    IEnumerator BarrierCooldown()
    {
        yield return new WaitForSeconds(barrierCounter);
        barrierUsed = false;
        Debug.Log("Barrier ready");
    }
    #endregion
}
