using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class DropItem : MonoBehaviour
{
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
       if(other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (playerIndex == 1 && gameObject.CompareTag("dropItem") )
                {
                    // set         public  bool player1IsHoldingItem1 to true in only on this players puzzleData
                    puzzleManager.pData.player1IsHoldingItem1 = true;
                    Destroy(this);
                    

                    
                }
                else if (playerIndex == 2 && gameObject.CompareTag("dropItem2"))
                {
                    // set         public  bool player2IsHoldingItem2 to true in only on this players puzzleData
                    puzzleManager.pData.player2IsHoldingItem1 = true;
                    Destroy(this);
                    
                }
                
                
            }
        }
    }
}
