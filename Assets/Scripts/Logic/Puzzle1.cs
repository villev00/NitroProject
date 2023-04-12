using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    PuzzleData puzzleData = new PuzzleData();
    
    [SerializeField] private GameObject brokenWall;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject rubble;
    [SerializeField] private AudioClip wallBreakClip;
    private AudioSource audioSource;
  
    private Animator anim;
    public float moveSpeed = 2f;
   
    private bool isPlayed = false;

    private void Start()
    {

        audioSource = brokenWall.GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.SetBool("down", false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {


            anim.SetBool("down", true);



        }
    }

    private void DestroyWall()
    {
        if (isPlayed == false)
        {

            wall.SetActive(false);
            rubble.SetActive(true);
            brokenWall.SetActive(true);

            audioSource.PlayOneShot(wallBreakClip);


            




        }



    }
}

