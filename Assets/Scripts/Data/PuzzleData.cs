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
        
        
        public bool Plattform1IsDown = false;
        public bool puzzleRune1IsActivated = false;
        

        public bool isSolved1;
        public bool isSolved2;
        public bool isSolved3;
        



    }
}