using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem fire;
    PuzzleData puzzleData;

    public Spell spellToTrigger;
    private void Awake()
    {
        
        fire.Stop();
    }

    public void Start()
    {
       
        
        
        puzzleData = PuzzleManager.instance.pData;

    }

   public void PlayParticle()
    {
     

        if (puzzleData.isSolved1) return;
        fire.Play();
        puzzleData.isSolved1 = true;
        PuzzleManager.instance.CheckPuzzle1();
        PuzzleManager.instance.CheckAllPuzzles();
       

    }
}
