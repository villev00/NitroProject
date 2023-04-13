using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;


public class runeRandom : MonoBehaviour
{
    [SerializeField] private GameObject runeHolder;
    [SerializeField] private GameObject fireRune;
    [SerializeField] private GameObject lightningRune;
    [SerializeField] private GameObject aetherRune;
    [SerializeField] private GameObject puzzle1PlattformRuneFire, puzzle1PlattformRuneLightning, puzzle1PlattformRuneAether;
    

    
    RuneData runeData;
    
    



    private void Start()
    {
        
        randomRune();
    }

    private void randomRune()
    {
        // set random rune active
        
        Random.InitState(System.DateTime.Now.Millisecond);
        int randomRune = Random.Range(0, 3);
        switch (randomRune)
        {
            case 0:
                fireRune.SetActive(true);
                puzzle1PlattformRuneFire.SetActive(true);
                break;
            case 1:
                lightningRune.SetActive(true);
                puzzle1PlattformRuneLightning.SetActive(true);
                break;
            case 2:
                aetherRune.SetActive(true);
                puzzle1PlattformRuneAether.SetActive(true);
                break;
        }
        
        
        
    }
  
 
   
 
   

           
           
        


        }
        


        
    
            




    
    
