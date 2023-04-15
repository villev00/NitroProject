using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public ParticleSystem fire;
    PuzzleManager puzzleManager;
     
    public void Start()
    {
        fire = GetComponent<ParticleSystem>();

       

        
        
    }

   private void PlayParticle()
    {
        fire.Play();
    }
}
