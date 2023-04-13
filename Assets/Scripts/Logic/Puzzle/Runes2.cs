using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class Runes2 : MonoBehaviour
{
    PuzzleData puzzleData;
    [SerializeField] private int thisRuneIndex;

    private void Awake()
    {
       Activate();
    }


    private void Start()
        {
            puzzleData = PuzzleManager.instance.pData;
            
        }
          
        public void Activate()
        {
            puzzleData.runeIndex = +1;
            thisRuneIndex = puzzleData.runeIndex;
        }
    
    
    
       public void OnTriggerEnter(Collider other)
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet == null) return;
    
            switch (gameObject.tag)
            {
                case "FireRune":
                    if (bullet.element == Element.Fire && thisRuneIndex == 1)
                    {
                        Debug.Log("SolvedFire");
                        puzzleData.puzzle2FireSloved = true;
                        puzzleData.puzzleStateIndex =+ 1;
                       
                       
                    }
                    break;
                case "LightningRune":
                    if (bullet.element == Element.Lightning &&  thisRuneIndex == 2)
                    {
                        Debug.Log("SolvedLightning");
                        puzzleData.puzzle2LightningSloved = true;
                        puzzleData.puzzleStateIndex =+ 1;
                        
                    }
                    break;
                case "AetherRune":
                    if (bullet.element == Element.Aether && thisRuneIndex == 3)
                    {
                        Debug.Log("SolvedAether");
                        puzzleData.puzzle2AetherSloved = true;
                        
                        
                    }
                    break;
                default:
                    break;
            }
        }
      
    }

