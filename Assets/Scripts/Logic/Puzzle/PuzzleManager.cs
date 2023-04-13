using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
   // create singleton
    public static PuzzleManager instance;
   public PuzzleData pData = new PuzzleData();
    
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
    
    public void CheckPuzzle1()
    {
        if (pData.isSolved1)
        {
            Debug.Log("Puzzle 1 solved");
        }
    }
    
    public void CheckPuzzle2()
    {
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
    

   
  
}
