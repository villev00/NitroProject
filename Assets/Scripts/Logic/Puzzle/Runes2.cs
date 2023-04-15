using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class Runes2 : MonoBehaviour
{
    [SerializeField]
    PuzzleData puzzleData;

    [SerializeField] AudioClip runeSolvedClip;
    [SerializeField] AudioClip runeFailedClip;
    
    private void Start()
        {
            puzzleData = PuzzleManager.instance.pData;
            
        }
          
       
    
    
       public void OnTriggerEnter(Collider other)
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet == null) return;
    
            switch (gameObject.tag)
            {
                case "FireRune":
                    if (bullet.element == Element.Fire && puzzleData.puzzleStateIndex == 1 )
                    {
                        Debug.Log("SolvedFire");
                        puzzleData.puzzle2FireSloved = true;
                        puzzleData.puzzleStateIndex += 1;
                        AudioManager.PlaySound(runeSolvedClip, false);
                        
                       
                       
                    }
                    else
                    {
                        Reset();
                        AudioManager.PlaySound(runeFailedClip, false);
                    }
                    break;
                case "LightningRune":
                    if (bullet.element == Element.Lightning && puzzleData.puzzleStateIndex == 2  )
                    {
                        Debug.Log("SolvedLightning");
                        puzzleData.puzzle2LightningSloved = true;
                        puzzleData.puzzleStateIndex += 1;
                        AudioManager.PlaySound(runeSolvedClip, false);
                        
                    }
                    else
                    {
                        Reset();
                        AudioManager.PlaySound(runeFailedClip, false);
                    }
                    break;
                case "AetherRune":
                    if (bullet.element == Element.Aether && puzzleData.puzzleStateIndex == 3 )
                    {
                        Debug.Log("SolvedAether");
                        puzzleData.puzzle2AetherSloved = true;
                        AudioManager.PlaySound(runeSolvedClip, false);


                    }
                    else
                    {
                        Reset();
                        AudioManager.PlaySound(runeFailedClip, false);
                    }
                    break;
                default:
                    break;
            }

        PuzzleManager.instance.CheckPuzzle2();
        PuzzleManager.instance.CheckAllPuzzles();
        }


       private void Reset()
       {
              puzzleData.puzzle2FireSloved = false;
              puzzleData.puzzle2LightningSloved = false;
              puzzleData.puzzle2AetherSloved = false;
              puzzleData.puzzleStateIndex = 1;
       }
}

