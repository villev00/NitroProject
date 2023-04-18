using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class PuzzleData 
    
    {
        public bool wallIsOpenPlayer1;
        public bool wallIsOpenPlayer2;
        
        public  GameObject puzzle1Plattform;
        public GameObject puzzle1Rune;
        
      

        public bool isSolved1;
        public bool isSolved2;
        public bool isSolved3;


        //public int runeIndex = 0;
        public int puzzleStateIndex = 0;
        //public List<GameObject> runePositions;
        //public List<GameObject> cluePositions;
        
        //public List<GameObject> runeElements;
        //public List<GameObject> clueElements;
        
      

        public bool puzzle2FireSloved = false;
        public bool puzzle2LightningSloved = false;
        public bool puzzle2AetherSloved = false;

        public bool allPuzzlesSolved;
        public bool hasOtherPlayerSolvedPuzzles;
        
        public bool fireSumon = false, lightningSumon = false, aetherSumon = false;
        
        public  bool player1PlacedItem1, player2PlacedItem1;
        public  bool player1IsHoldingItem1, player2IsHoldingItem1;
        
        public GameObject dropItem1, dropItem2;
        public Transform dropPoint1, dropPoint2;
        public GameObject spawnObject1, spawnObject2;
        public GameObject itemPlacment1, itemPlacment2;
        public GameObject placedItem1, placedItem2;
        

    }
}