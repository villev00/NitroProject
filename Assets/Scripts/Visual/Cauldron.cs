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
        fire.Play();
        puzzleData.isSolved1 = true;
    }
}
