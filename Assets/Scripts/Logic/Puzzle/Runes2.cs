using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class Runes2 : MonoBehaviour
{
    [SerializeField]
    private int fireIndex;
    [SerializeField]
    private int lightningIndex;
    [SerializeField]
    private int aetherIndex;
    [SerializeField]
    PuzzleData puzzleData;
    [SerializeField] AudioClip correctAnswer, wrongAnswer;
    private void Start()
        {
            puzzleData = PuzzleManager.instance.pData;
            
        }
            
       public void OnTriggerEnter(Collider other)
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet == null || puzzleData.isSolved2) return;
    
            switch (gameObject.tag)
            {
                case "FireRune":
                    if (bullet.element == Element.Fire && puzzleData.puzzleStateIndex == fireIndex )
                    {
                        Debug.Log("SolvedFire");
                        puzzleData.puzzle2FireSloved = true;
                        puzzleData.puzzleStateIndex += 1;
                        AudioManager.PlaySound(correctAnswer, false);
                       
                       
                    }
                    else
                    {
                        Reset();
                      
                    }
                    break;
                case "LightningRune":
                    if (bullet.element == Element.Lightning && puzzleData.puzzleStateIndex == lightningIndex  )
                    {
                        Debug.Log("SolvedLightning");
                        puzzleData.puzzle2LightningSloved = true;
                        puzzleData.puzzleStateIndex += 1;
                        AudioManager.PlaySound(correctAnswer, false);
                    }
                    else
                    {
                        Reset();
                    }
                    break;
                case "AetherRune":
                    if (bullet.element == Element.Aether && puzzleData.puzzleStateIndex == aetherIndex )
                    {
                        Debug.Log("SolvedAether");
                        puzzleData.puzzle2AetherSloved = true;
                      //  AudioManager.PlaySound(correctAnswer, false);

                }
                    else
                    {
                        Reset();
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
            AudioManager.PlaySound(wrongAnswer, false);
            puzzleData.puzzle2FireSloved = false;
              puzzleData.puzzle2LightningSloved = false;
              puzzleData.puzzle2AetherSloved = false;
              puzzleData.puzzleStateIndex = 1;
       }
}

