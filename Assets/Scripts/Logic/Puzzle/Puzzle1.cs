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
    [SerializeField] private GameObject [] puzzle1StateOn1;
    [SerializeField] private GameObject [] puzzle1StateOff1;
    [SerializeField] private GameObject [] puzzle1StateOn2;
    [SerializeField] private GameObject [] puzzle1StateOff2;

    
  
    [SerializeField] private float speed;
    
    PhotonView pv;

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
           // puzzleData.playerStanding = true;
            if(puzzleData.playersOnPlatform == 0)
            {
                puzzle1StateOff1[0].SetActive(false);
                puzzle1StateOn1[0].SetActive(true);
                puzzle1StateOff1[1].SetActive(false);
                puzzle1StateOn1[1].SetActive(true);
            }
            if (puzzleData.playersOnPlatform == 2)
            {
                puzzle1StateOff2[0].SetActive(false);
                puzzle1StateOn2[0].SetActive(true);
                puzzle1StateOff2[1].SetActive(false);
                puzzle1StateOn2[1].SetActive(true);
            }
            puzzleData.playersOnPlatform++;
            Debug.Log("Player entered the trigger");

           // other.GetComponent<PuzzleSolver>().otherPlayerStanding();

          
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // Start moving the platform towards the stop position
        if (other.CompareTag("Player") &&!isMoving && puzzleData.playersOnPlatform == 4)
        {
            DisableVisualEffect();
            StartCoroutine(MovePlatform());
            isMoving = true;
            player = other.gameObject;           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            // puzzleData.playerStanding = false;
            puzzleData.playersOnPlatform--;
            if (puzzleData.playersOnPlatform == 0)
            {
                puzzle1StateOff1[0].SetActive(true);
                puzzle1StateOn1[0].SetActive(false);
                puzzle1StateOff1[1].SetActive(true);
                puzzle1StateOn1[1].SetActive(false);
            }
            if (puzzleData.playersOnPlatform == 2)
            {
                puzzle1StateOff2[0].SetActive(true);
                puzzle1StateOn2[0].SetActive(false);
                puzzle1StateOff2[1].SetActive(true);
                puzzle1StateOn2[1].SetActive(false);
            }
            // other.GetComponent<PhotonView>().RPC("RPC_otherPlayerLightsOff", RpcTarget.Others);



        }
    }

    private IEnumerator MovePlatform()
    {
        // Move the platform towards the stop position over time
        while (transform.position != stopPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, stopPosition, speed * Time.deltaTime);          
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
    }
    
    public void otherPlayerLights()
    {
       // puzzle1StateOff2.SetActive(false);
       // puzzle1StateOn2.SetActive(true);
    }
    [PunRPC]
    public void RPC_otherPlayerLightsOff()
    {
        Debug.Log("RPC_otherPlayerLightsOff");
       // puzzle1StateOff2.SetActive(true);
       // puzzle1StateOn2.SetActive(false);
    }
    
    
   

    public void DisableVisualEffect()
    {
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }
}


