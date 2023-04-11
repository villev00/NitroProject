using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    PuzzleData puzzleData = new PuzzleData();
    [SerializeField] private GameObject puzzle1Plattform;
    [SerializeField] private GameObject brokenWall;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject rubble;
    [SerializeField] private AudioClip wallBreakClip;
    private AudioSource audioSource;
    [SerializeField] Rigidbody rb;

    public float moveSpeed = 2f;
    public float stopPosition = 0.2f;
    private bool isPlayed = false;

    private void Start()
    {

        audioSource = brokenWall.GetComponent<AudioSource>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb.useGravity = true;
            // Move the platform down
            puzzle1Plattform.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

            // Check if the platform has reached the stop position
            if (puzzle1Plattform.transform.position.y <= stopPosition)
            {
                // If the platform has reached the stop position, set its position to the stop position
                Vector3 newPos = puzzle1Plattform.transform.position;
                newPos.y = stopPosition;
                puzzle1Plattform.transform.position = newPos;

                // Call the method that destroys the wall and plays the sound clip
                DestroyWall();
            }
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


            puzzle1Plattform.isStatic = true;
            isPlayed = true;




        }



    }
}

