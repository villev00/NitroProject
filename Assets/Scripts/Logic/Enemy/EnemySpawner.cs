using System;
using System.Collections;
using Data;
using UnityEngine;
using Photon.Pun;



namespace Logic.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform[] spawnPoint1;
        [SerializeField]
        private  Transform[] spawnPoint2;

        [SerializeField]
        private SpawnData spawnData = new SpawnData();

        [SerializeField] private PuzzleData puzzleData;
       
        private bool stopSpawning = false;
        int playerIndex;
              
        void Start()
        {
            InvokeRepeating("minusEnemyCount", 5, 5);
            playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;
            // get puzzle data from puzzle manager
            puzzleData = PuzzleManager.instance.pData;
            spawnData.enemyList.Add(spawnData.fireEnemyMelee);
            spawnData.enemyList.Add(spawnData.fireEnemyRanged);
            spawnData.enemyList.Add(spawnData.aetherEnemyMelee);
            spawnData.enemyList.Add(spawnData.aetherEnemyRanged);
            
            InvokeRepeating("minuSpawnCount",5,5);

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
                stopSpawning = true;
                
            }

            if (puzzleData.isSolved3 == true)
            {
                stopSpawning = true;
            }
        }
        
        public void SpawnEnemy()
        {
            int randomEnemy = UnityEngine.Random.Range(0, spawnData.enemyList.Count);
            int randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoint1.Length);
            int randomSpawnPoint2 = UnityEngine.Random.Range(0, spawnPoint2.Length);
            Instantiate(spawnData.enemyList[randomEnemy], spawnPoint1[randomSpawnPoint].position, Quaternion.identity);
            Instantiate(spawnData.enemyList[randomEnemy], spawnPoint2[randomSpawnPoint2].position, Quaternion.identity);
        }       

       private IEnumerator SpawnEnemyCoroutine()
       {
           
            while (spawnData.spawnCount < spawnData.maxSpawnCount && !stopSpawning)
            {
                int randomEnemyIndex = UnityEngine.Random.Range(0, spawnData.enemyList.Count);
                if (playerIndex == 1)
                {
                    int randomSpawnPointIndex1 = UnityEngine.Random.Range(0, spawnPoint1.Length);
                    Instantiate(spawnData.enemyList[randomEnemyIndex], spawnPoint1[randomSpawnPointIndex1].position, Quaternion.identity);
                }
                else if (playerIndex == 2)
                {
                    int randomSpawnPointIndex2 = UnityEngine.Random.Range(0, spawnPoint2.Length);
                    Instantiate(spawnData.enemyList[randomEnemyIndex], spawnPoint2[randomSpawnPointIndex2].position, Quaternion.identity);
                }
                
                
                
                spawnData.spawnCount++;
                yield return new WaitForSeconds(spawnData.spawnRate);
            }
       }
       
       
       public void minuSpawnCount()
       {
          
              spawnData.spawnCount--;
              
              
       }

      
    }
}