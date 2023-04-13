using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRunes2 : MonoBehaviour
{
    [SerializeField]    
    PuzzleData puzzleData;
    
    private void Start()
    {
        puzzleData = PuzzleManager.instance.pData;
        RandomRunePosition();
    }
    
    // activate puzzleElement to random runePosition
    
    void RandomRunePosition()
    {
       // get random gamobject from list runePositions
       
       int randomIndexPostion = Random.Range(0, puzzleData.runePositions.Count);
       
       int randomIndexElement = Random.Range(0, puzzleData.runeElements.Count);
        
       // activate child of runePosition
         puzzleData.runePositions[randomIndexPostion].transform.GetChild(randomIndexElement).gameObject.SetActive(true);
         
            // remove runePosition from list
            puzzleData.runePositions.RemoveAt(randomIndexPostion);
            // remove runeElement from list
            puzzleData.runeElements.RemoveAt(randomIndexElement);
    }
    
   
}
