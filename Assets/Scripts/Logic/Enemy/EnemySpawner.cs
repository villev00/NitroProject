using System;
using System.Collections;
using Data;
using UnityEngine;



namespace Logic.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform[] spawnPoint;

        [SerializeField]
        private SpawnData spawnData = new SpawnData();
        [SerializeField]
        private PuzzleData puzzleData = new PuzzleData();

        private bool stopSpawning = false;
        
        
        
        void Start()
        {
            
            spawnData.enemyList.Add(spawnData.fireEnemyMelee);
            spawnData.enemyList.Add(spawnData.fireEnemyRanged);
            
           // add deylay to start spawning enemies 
           
           
           
           
           StartCoroutine(SpawnEnemyCoroutine());
        }


        public void addEnemyToList()
        {
            // after puzzle is solved add enemy to list and spawn from list at start fire enemy


            if (puzzleData.isSolved1 == true )
            {
                spawnData.enemyList.Add(spawnData.lightningEnemyMelee);
                spawnData.enemyList.Add(spawnData.lightningEnemyRanged);
                
            }

            if (puzzleData.isSolved2 == true)
            {
                spawnData.enemyList.Add(spawnData.aetherEnemyMelee);
                spawnData.enemyList.Add(spawnData.aetherEnemyRanged);
                
            }
            if (puzzleData.isSolved3 == true)
            {
                stopSpawning = true;


            }
            
            
        }
        
        public void spawnEnemy()
        {
            int randomEnemy = UnityEngine.Random.Range(0, spawnData.enemyList.Count);
            int randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoint.Length);
            Instantiate(spawnData.enemyList[randomEnemy], spawnPoint[randomSpawnPoint].position, Quaternion.identity);
        }

       private IEnumerator SpawnEnemyCoroutine()
    {
        while (spawnData.spawnCount < spawnData.maxSpawnCount && !stopSpawning)
        {
            int randomEnemyIndex = UnityEngine.Random.Range(0, spawnData.enemyList.Count);
            int randomSpawnPointIndex = UnityEngine.Random.Range(0, spawnPoint.Length);
            Instantiate(spawnData.enemyList[randomEnemyIndex], spawnPoint[randomSpawnPointIndex].position, Quaternion.identity);
            spawnData.spawnCount++;
            yield return new WaitForSeconds(spawnData.spawnRate);
        }
    }

        private void Update()
        {
            

            if (Input.GetKeyDown(KeyCode.J))
            {
                puzzleData.isSolved1 = true;
                addEnemyToList();

            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                puzzleData.isSolved2 = true;
                addEnemyToList();

            }
            if(Input.GetKeyDown(KeyCode.K))
            {
                puzzleData.isSolved3 = true;
                
            }

            



        }









    }
}