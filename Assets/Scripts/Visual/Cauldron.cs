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
    [SerializeField] private GameObject puzzle1StateOn2;
    [SerializeField] private GameObject puzzle1StateOff2;


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
        puzzle1StateOff2.SetActive(false);
        puzzle1StateOn2.SetActive(true);

        if (puzzleData.isSolved1) return;
        fire.Play();
        puzzleData.isSolved1 = true;
        PuzzleManager.instance.CheckPuzzle1();
        PuzzleManager.instance.CheckAllPuzzles();
       

    }
}
