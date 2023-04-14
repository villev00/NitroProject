using System;
using Data;
using System.Collections;
using System.Collections.Generic;
using Logic.Enemy;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PuzzleManager : MonoBehaviour
{
   // create singleton
    public static PuzzleManager instance;
   public PuzzleData pData = new PuzzleData();
   
  
    //public event System.Action OnPuzzle3Solved;

   private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   private void Update()
   {
         CheckPuzzle1();
         CheckPuzzle2();
         //CheckPuzzle3();
         //CheckAllPuzzles();
   }

   public void CheckPuzzle1()
    {
        
        if (pData.isSolved1)
        {
            Debug.Log("Puzzle 1 solved");
        }
    }
    
    public void CheckPuzzle2()
    {
        if(pData.puzzle2FireSloved && pData.puzzle2LightningSloved && pData.puzzle2AetherSloved)
        {
            pData.isSolved2 = true;

        }
      
        if (pData.isSolved2)
        {
            
            Debug.Log("Puzzle 2 solved");
        }
    }
    
    public void CheckPuzzle3()
    {
        if (pData.isSolved3)
        {
            Debug.Log("Puzzle 3 solved");
        }
    }
    
    public void CheckAllPuzzles()
    {
        if (pData.isSolved1 && pData.isSolved2 && pData.isSolved3)
        {
            Debug.Log("All puzzles solved");
        }
    }
    
    public void bothPlayersPuzzlesSolved()
    {
    }

    

   
  
}
