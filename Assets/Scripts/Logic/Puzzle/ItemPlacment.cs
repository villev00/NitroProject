using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPlacment : MonoBehaviour
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
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (playerIndex == 1 && puzzleManager.pData.player1IsHoldingItem1 ) 
                {
                    // set itemPlacment1 to false and placedItem1 to true in only on this players puzzleData
                   
                    puzzleManager.pData.itemPlacment1.SetActive(false);
                    puzzleManager.pData.placedItem1.SetActive(true);
                    
                    

                }else if (playerIndex == 2)
                {
                    puzzleManager.pData.itemPlacment2.SetActive(false);
                    puzzleManager.pData.placedItem2.SetActive(true);
                    
                }

            }
        }
    }
}

