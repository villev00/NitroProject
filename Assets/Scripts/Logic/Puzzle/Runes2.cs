using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class Runes2 : MonoBehaviour
{
    [SerializeField]
    PuzzleData puzzleData;
    
    

    


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
                        
                       
                       
                    }
                    break;
                case "LightningRune":
                    if (bullet.element == Element.Lightning && puzzleData.puzzleStateIndex == 2  )
                    {
                        Debug.Log("SolvedLightning");
                        puzzleData.puzzle2LightningSloved = true;
                        puzzleData.puzzleStateIndex += 1;
                        
                    }
                    break;
                case "AetherRune":
                    if (bullet.element == Element.Aether && puzzleData.puzzleStateIndex == 3 )
                    {
                        Debug.Log("SolvedAether");
                        puzzleData.puzzle2AetherSloved = true;


                    }
                    break;
                default:
                    break;
            }

        PuzzleManager.instance.CheckPuzzle2();
        PuzzleManager.instance.CheckAllPuzzles();
        }
      
    }

