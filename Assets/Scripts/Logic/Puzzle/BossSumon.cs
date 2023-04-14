using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class BossSumon : MonoBehaviour
{
    PuzzleData puzzleData;
    

    public int bossIndex = 0;

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
                if (bullet.element == Element.Fire && bossIndex == 0)
                {
                    Debug.Log("SolvedFire");
                    puzzleData.fireSumon = true;
                    bossIndex += 1;




                }
                else
                {
                    Reset();
                }

                break;
            case "LightningRune":
                if (bullet.element == Element.Lightning && bossIndex == 1)
                {
                    Debug.Log("SolvedLightning");
                    puzzleData.lightningSumon = true;
                    bossIndex += 1;

                }
                else
                {
                    Reset();
                }

                break;
            case "AetherRune":
                if (bullet.element == Element.Aether && bossIndex == 2)
                {
                    puzzleData.aetherSumon = true;

                    Debug.Log("SolvedAether");
                    bossIndex += 1;



                }
                else
                {
                    Reset();
                }

                break;
            default:
                break;
        }

        void Reset()
        {

           puzzleData.fireSumon = false;
           puzzleData.lightningSumon = false;
           puzzleData.aetherSumon = false;
           bossIndex = 0;

        }
        
        void SumonBoss()
        {
            if (puzzleData.fireSumon && puzzleData.lightningSumon && puzzleData.aetherSumon)
            {
                Debug.Log("Boss Sumon");
            }
        }

    }
}
