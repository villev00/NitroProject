using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class SpawnData 
    {
        public bool isSpwaning = false;
        
        public GameObject fireEnemyMelee;
        public GameObject lightningEnemyMelee;
        public GameObject aetherEnemyMelee;
        
        public GameObject fireEnemyRanged;
        public GameObject lightningEnemyRanged;
        public GameObject aetherEnemyRanged;
        
        public List<GameObject> enemyList;

       
        public float spawnRate = 3f;

        public int spawnCount = 0;
        public int maxSpawnCount = 20;
        
        
    
    }

    
}
