using System;
using Data;
using System.Collections;
using System.Collections.Generic;
using Logic.Enemy;
using UnityEngine;
using Photon.Pun;

public class PuzzleManager : MonoBehaviour
{
   // create singleton
    public static PuzzleManager instance;
    public PuzzleData pData = new PuzzleData();

    public GameObject player;
    public GameObject SpawnPoint;
    //public event System.Action OnPuzzle3Solved;

    [SerializeField] AudioClip puzzleCompleteSound;
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

   private void Start()
   {
       InvokeRepeating("ActivateSpanwer", 5, 5);
   }

   public void CheckPuzzle1()
    {      
        if (pData.isSolved1)
        {
            Debug.Log("Puzzle 1 solved");
            player.GetComponent<PuzzleSolver>().OtherSolvedPuzzles();
            AudioManager.PlaySound(puzzleCompleteSound, false);
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
            AudioManager.PlaySound(puzzleCompleteSound, false);
            Debug.Log("Puzzle 2 solved");
            CheckAllPuzzles();
            player.GetComponent<PuzzleSolver>().OtherSolvedPuzzles();
        }
    }
    
    public void CheckPuzzle3()
    {
        if (pData.isSolved3)
        {
            AudioManager.PlaySound(puzzleCompleteSound, false);
            Debug.Log("Puzzle 3 solved");
        }
    }

  
    public void CheckAllPuzzles()
    {
       
        if (pData.isSolved1 && pData.isSolved2) //&& pData.isSolved3)
        {
            Debug.Log("All puzzles solved");
            pData.allPuzzlesSolved = true;

            if (pData.hasOtherPlayerSolvedPuzzles)
            {
                Debug.Log("Other player solved all");               
            }               
        }       
    }
    
    public void OpenFences()
    {                                
                GameObject.FindGameObjectWithTag("Fence").GetComponent<FenceClose>().OpenFence();
                GameObject.FindGameObjectWithTag("Fence2").GetComponent<FenceClose>().OpenFence();                    
    }
    
    public  void ActivateSpanwer()
    {
        if (pData.bothPlayersWallWasDestroyed)
        {
              SpawnPoint.SetActive(true);
        }
      
    }
}
