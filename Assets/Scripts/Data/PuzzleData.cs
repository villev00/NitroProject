using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class PuzzleData 
    
    {
        public  GameObject puzzle1Plattform;
        public GameObject puzzle1Rune;
        
        
        
        

        public bool isSolved1;
        public bool isSolved2;
        public bool isSolved3;


        public int runeIndex = 0;
        public int puzzleStateIndex = 0;
        public List<GameObject> runePositions;
        public List<GameObject> cluePositions;
        
        public List<GameObject> runeElements;
        public List<GameObject> clueElements;
        
        

        public bool puzzle2FireSloved = false;
        public bool puzzle2LightningSloved = false;
        public bool puzzle2AetherSloved = false;




    }
}