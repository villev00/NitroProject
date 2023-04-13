using System;
using Data;
using UnityEngine;

public class Runes : MonoBehaviour
{
    PuzzleData puzzleData;
    
    private void Start()
    {
        puzzleData = PuzzleManager.instance.pData;
    }



   public void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet == null) return;

        switch (gameObject.tag)
        {
            case "FireRune":
                if (bullet.element == Element.Fire)
                {
                    Debug.Log("SolvedFire");
                    puzzleData.isSolved1 = true;
                   
                }
                break;
            case "LightningRune":
                if (bullet.element == Element.Lightning)
                {
                    Debug.Log("SolvedLightning");
                    puzzleData.isSolved1 = true;
                }
                break;
            case "AetherRune":
                if (bullet.element == Element.Aether)
                {
                    Debug.Log("SolvedAether");
                    puzzleData.isSolved1 = true;
                   
                }
                break;
            default:
                break;
        }
    }
  
}