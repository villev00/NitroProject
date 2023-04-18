using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class wallSolved : MonoBehaviour
{
    private PuzzleData puzzleData;
    [SerializeField] GameObject spawnPoint;

    private void Start()
    {
        puzzleData = PuzzleManager.instance.pData;
        InvokeRepeating("checkWalls", 5, 5);
    }

    private void checkWalls()
    {
        if (puzzleData.wallIsOpenPlayer1 && puzzleData.wallIsOpenPlayer2)
        {
            spawnPoint.SetActive(true);
        }
    }
}
