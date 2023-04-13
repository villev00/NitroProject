using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    private PuzzleData PuzzleData;
   
    
    [SerializeField] private GameObject brokenWall;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject rubble;
    [SerializeField] private AudioClip wallBreakClip;
    private AudioSource audioSource;
  
    private  Animation anim;
    public float moveSpeed = 2f;
   
  

    private void Start()
    {
        PuzzleData = PuzzleManager.instance.pData;
        anim = GetComponent<Animation>();

        audioSource = brokenWall.GetComponent<AudioSource>();
       
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered");

            
        }
    }

    private void DestroyWall()
    {
      
        

            wall.SetActive(false);
            rubble.SetActive(true);
            brokenWall.SetActive(true);

            audioSource.PlayOneShot(wallBreakClip);


            




        



    }
}

