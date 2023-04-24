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
        public  bool otherPlayerWallWasDestroyed;
        public  bool bothPlayersWallWasDestroyed;
      

        public bool isSolved1;
        public bool isSolved2;
        public bool isSolved3;

        public bool playerStanding;
        public bool otherPlayerStanding;
        public bool bothPlayersStanding;

        //public int runeIndex = 0;
        public int puzzleStateIndex = 0;
        //public List<GameObject> runePositions;
        //public List<GameObject> cluePositions;

        //public List<GameObject> runeElements;
        //public List<GameObject> clueElements;

        public int playersOnPlatform;

        public bool puzzle2FireSloved = false;
        public bool puzzle2LightningSloved = false;
        public bool puzzle2AetherSloved = false;

        public bool allPuzzlesSolved;
        public bool hasOtherPlayerSolvedPuzzles;
        
        public bool fireSumon = false, lightningSumon = false, aetherSumon = false;

    }
}