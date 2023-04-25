using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class PuzzleData 
    
    {
        public bool wallWasDestroyed;
     

        public bool isSolved1;
        public bool isSolved2;
      
        public int puzzleStateIndex = 0;
        public int playersOnPlatform;

        public bool puzzle2FireSloved = false;
        public bool puzzle2LightningSloved = false;
        public bool puzzle2AetherSloved = false;

        public bool allPuzzlesSolved;
        public bool hasOtherPlayerSolvedPuzzles;
        
        public bool fireSumon = false, lightningSumon = false, aetherSumon = false;

    }
}