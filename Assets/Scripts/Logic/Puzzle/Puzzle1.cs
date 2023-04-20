using System.Collections;
using Data;
using Photon.Pun;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    // Private fields
    private PuzzleData puzzleData;
    private bool isMoving = false;
    private BoxCollider boxCollider;

    // Serialized fields
    [SerializeField] GameObject puzzleCauldron;

    [SerializeField] private GameObject brokenWall;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject rubble;
    [SerializeField] private AudioClip wallBreakClip;
    [SerializeField] private Vector3 stopPosition;
    [SerializeField] private GameObject puzzle1StateOn1;
    [SerializeField] private GameObject puzzle1StateOff1;
    
  
    [SerializeField] private float speed;

    // Events
    public event System.Action OnPuzzleSolved;


    GameObject player;

    private void Start()
    {
        // Get the PuzzleData from the PuzzleManager
        puzzleData = PuzzleManager.instance.pData;

        // Cache the reference to the BoxCollider component
        boxCollider = GetComponent<BoxCollider>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");
            DisableVisualEffect();
            // Start moving the platform towards the stop position
            if (!isMoving)
            {
                StartCoroutine(MovePlatform());
                isMoving = true;
                player = other.gameObject;
               

            }
        }
    }

    private IEnumerator MovePlatform()
    {
        // Move the platform towards the stop position over time
        while (transform.position != stopPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, stopPosition, speed * Time.deltaTime);
            puzzle1StateOff1.SetActive(false); 
            puzzle1StateOn1.SetActive(true);
            yield return null;
        }

        // Disable the BoxCollider and destroy the wall
        boxCollider.enabled = false;

        player.GetComponent<PuzzleSolver>().DestroyWall();
        // Invoke the OnPuzzleSolved event
        //  OnPuzzleSolved?.Invoke();
    }

    public void DestroyWall()
    {
        // Play the wall break sound effect
        AudioManager.PlaySound(wallBreakClip,false);
        // Hide the intact wall and show the broken wall and rubble
        wall.SetActive(false);
        brokenWall.SetActive(true);
        rubble.SetActive(true);
        puzzleData.otherPlayerWallWasDestroyed = true;
        
        
    }

    public void DisableVisualEffect()
    {
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }
}


