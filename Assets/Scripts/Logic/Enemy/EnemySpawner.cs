﻿using System;
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
        private Transform bossSpawnPoint;

        [SerializeField]
        private SpawnData spawnData = new SpawnData();

        [SerializeField]
        private GameObject boss;

        [SerializeField] private PuzzleData puzzleData;
       

        private bool stopSpawning = false;
        
        
        
        void Start()
        {
            // get puzzle data from puzzle manager
            puzzleData = PuzzleManager.instance.pData;
            spawnData.enemyList.Add(spawnData.fireEnemyMelee);
            spawnData.enemyList.Add(spawnData.fireEnemyRanged);

            //SpawnBoss();
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
        
        public void SpawnEnemy()
        {
            int randomEnemy = UnityEngine.Random.Range(0, spawnData.enemyList.Count);
            int randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoint.Length);
            Instantiate(spawnData.enemyList[randomEnemy], spawnPoint[randomSpawnPoint].position, Quaternion.identity);
        }

        public void SpawnBoss()
        {
            Instantiate(boss, bossSpawnPoint);
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

            //if (Input.GetKeyDown(KeyCode.J))
            //{
            //    puzzleData.isSolved1 = true;
            //    addEnemyToList();
            //}

            //if (Input.GetKeyDown(KeyCode.H))
            //{
            //    puzzleData.isSolved2 = true;
            //    addEnemyToList();
            //}

            //if (Input.GetKeyDown(KeyCode.K))
            //{
            //    puzzleData.isSolved3 = true;
            //}

        }
    }
}