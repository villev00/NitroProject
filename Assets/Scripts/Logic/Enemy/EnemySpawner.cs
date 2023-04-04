using System;
using System.Collections;
using Data;
using UnityEngine;



namespace Logic.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private  Transform[] spawnPoint; 
        
        [SerializeField]
        private SpawnData spawndata = new SpawnData();
        [SerializeField]
        private PuzzleData puzzleData = new PuzzleData();
        
        
        
        void Start()
        {
            
            spawndata.enemyList.Add(spawndata.fireEnemyMelee);
            spawndata.enemyList.Add(spawndata.fireEnemyRanged);
            StartCoroutine(spawnEnemyCoroutine());
        }


        public void addEnemyToList()
        {
            // after puzzle is solved add enemy to list and spawn from list at start fire enemy


            if (puzzleData.isSolved1 == true)
            {
                spawndata.enemyList.Add(spawndata.lightningEnemyMelee);
                spawndata.enemyList.Add(spawndata.lightningEnemyRanged);
            }

            if (puzzleData.isSolved2 == true)
            {
                spawndata.enemyList.Add(spawndata.aetherEnemyMelee);
                spawndata.enemyList.Add(spawndata.aetherEnemyRanged);
            }
            
            
        }
        
        public void spawnEnemy()
        {
            int randomEnemy = UnityEngine.Random.Range(0, spawndata.enemyList.Count);
            int randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoint.Length);
            Instantiate(spawndata.enemyList[randomEnemy], spawnPoint[randomSpawnPoint].position, Quaternion.identity);
        }

        private IEnumerator spawnEnemyCoroutine()
        {
            while (spawndata.spawnCount < spawndata.maxSpawnCount)
            {
                spawnEnemy();
                spawndata.spawnCount++;
                yield return new WaitForSeconds(spawndata.spawnRate);
            }
        }

        private void Update()
        {
            

            if (Input.GetKeyDown(KeyCode.J))
            {
                puzzleData.isSolved1 = true;
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                puzzleData.isSolved2 = true;
            }
            addEnemyToList();



        }









    }
}