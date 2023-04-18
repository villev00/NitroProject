using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;



public class puzzle3 : MonoBehaviour
{
    // Start is called before the first frame update
    PuzzleManager puzzleManager;
    PhotonView pv;

    int playerIndex;

    void Start()
    {
        puzzleManager = PuzzleManager.instance;
        pv = GetComponent<PhotonView>();
        playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;


    }


    private void OnTriggerStay(Collider other)
    {

        // only the player who is the owner of the photon view can interact with the puzzle

        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (playerIndex == 1 && puzzleManager.pData.player1IsHoldingItem1)
                {
                   
                    // spawn the spawnObject2 on the other players  dropPoint2
                    pv.RPC("RPC_SpawnObject2", RpcTarget.Others, puzzleManager.pData.spawnObject2,
                        puzzleManager.pData.dropPoint2);
                }
                else if (playerIndex == 2 && puzzleManager.pData.player2IsHoldingItem1)
                {
                    // spawn the spawnObject2 on the other players  dropPoint2
                    pv.RPC("RPC_SpawnObject2", RpcTarget.Others, puzzleManager.pData.spawnObject2,
                        puzzleManager.pData.dropPoint2);
                }
            }
        }
        
        
        
    }
}
