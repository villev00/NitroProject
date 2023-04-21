using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class Runes2 : MonoBehaviour
{
    [SerializeField]
    private int fireIndex;
    [SerializeField]
    private int lightningIndex;
    [SerializeField]
    private int aetherIndex;
    [SerializeField]
    PuzzleData puzzleData;
    [SerializeField] Light[] puzzleLights = new Light[3];
    [SerializeField]
    GameObject[] players;

    public string tag;
    private void Start()
    {
        puzzleData = PuzzleManager.instance.pData;
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet == null || puzzleData.isSolved2) return;
        if (puzzleData.bothPlayersWallWasDestroyed)
        {
            switch (tag)
            {
                case "FireRune":
                    if (bullet.element == Element.Fire && puzzleData.puzzleStateIndex == fireIndex)
                    {
                        puzzleData.puzzle2FireSloved = true;
                        puzzleData.puzzleStateIndex += 1;
                        puzzleLights[1].intensity = 3;

                    }
                    else
                    {
                        Reset();

                    }

                    break;
                case "LightningRune":
                    if (bullet.element == Element.Lightning && puzzleData.puzzleStateIndex == lightningIndex)
                    {
                        puzzleData.puzzle2LightningSloved = true;
                        puzzleData.puzzleStateIndex += 1;
                        puzzleLights[0].intensity = 1.5f;
                    }
                    else
                    {
                        Reset();
                    }

                    break;
                case "AetherRune":
                    if (bullet.element == Element.Aether && puzzleData.puzzleStateIndex == aetherIndex)
                    {
                        puzzleData.puzzle2AetherSloved = true;
                        puzzleLights[2].intensity = 5;

                    }
                    else
                    {
                        Reset();
                    }

                    break;
                default:
                    break;
            }

            PuzzleManager.instance.CheckPuzzle2();
            PuzzleManager.instance.CheckAllPuzzles();
        }

    }


    private void Reset()
    {
        for (int i = 0; i < puzzleLights.Length; i++)
        {
            puzzleLights[i].intensity = 1;
        }
        puzzleData.puzzle2FireSloved = false;
        puzzleData.puzzle2LightningSloved = false;
        puzzleData.puzzle2AetherSloved = false;
        puzzleData.puzzleStateIndex = 1;
        players = GameObject.FindGameObjectsWithTag("Player");
        players[0].GetComponent<PlayerLogic>().TakeDamage(20);
        players[1].GetComponent<PlayerLogic>().TakeDamage(20);
    }
}