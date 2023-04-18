using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class Runes2 : MonoBehaviour
{
    [SerializeField] private int fireIndex;
    [SerializeField] private int lightningIndex;
    [SerializeField] private int aetherIndex;
    [SerializeField] private PuzzleData puzzleData;
    [SerializeField] private AudioClip correctAnswer;
    [SerializeField] private AudioClip wrongAnswer;

    private Bullet bullet;

    private void Start()
    {
        puzzleData = PuzzleManager.instance.pData;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (puzzleData.wallIsOpenPlayer1 || puzzleData.wallIsOpenPlayer2)
        {
            bullet = other.GetComponent<Bullet>();
            if (bullet == null || puzzleData.isSolved2) return;

            switch (gameObject.tag)
            {
                case "FireRune":
                    SolvePuzzle(Element.Fire, fireIndex);
                    break;
                case "LightningRune":
                    SolvePuzzle(Element.Lightning, lightningIndex);
                    break;
                case "AetherRune":
                    SolvePuzzle(Element.Aether, aetherIndex);
                    break;
                default:
                    break;
            }

            PuzzleManager.instance.CheckPuzzle2();
            PuzzleManager.instance.CheckAllPuzzles();
        }
    }

    private void SolvePuzzle(Element element, int puzzleIndex)
    {
        if (bullet.element == element && puzzleData.puzzleStateIndex == puzzleIndex)
        {
            Debug.Log("Solved" + element.ToString());
            switch (element)
            {
                case Element.Fire:
                    puzzleData.puzzle2FireSloved = true;
                    break;
                case Element.Lightning:
                    puzzleData.puzzle2LightningSloved = true;
                    break;
                case Element.Aether:
                    puzzleData.puzzle2AetherSloved = true;
                    break;
            }

            puzzleData.puzzleStateIndex++;
            AudioManager.PlaySound(correctAnswer, false);
        }
        else
        {
            Reset();
        }
    }

    private void Reset()
    {
        AudioManager.PlaySound(wrongAnswer, false);
        puzzleData.puzzle2FireSloved = false;
        puzzleData.puzzle2LightningSloved = false;
        puzzleData.puzzle2AetherSloved = false;
        puzzleData.puzzleStateIndex = 1;
    }
}
