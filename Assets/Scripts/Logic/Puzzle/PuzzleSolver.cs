using Data;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    PhotonView pv;
    int playerIndex;
    public GameObject puzzle1;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        InvokeRepeating("CheckAll", 5, 5);
    }
    void Start()
    {

        if (!pv.IsMine) return;
        puzzle1 = GameObject.Find("Puzzle1_World" + PhotonNetwork.LocalPlayer.ActorNumber);
        PuzzleManager.instance.player = gameObject;
        playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;
          
    }

    public void OtherSolvedPuzzles()
    {
        if (PuzzleManager.instance.pData.allPuzzlesSolved)
            gameObject.GetComponent<PlayerLogic>().otherPlayer.GetComponent<PhotonView>().RPC("RPC_AllSolved", RpcTarget.Others);
    }
    public void DestroyWall()
    {
       
        if (pv.IsMine)
            gameObject.GetComponent<PlayerLogic>().otherPlayer.GetComponent<PhotonView>().RPC("RPC_DestroyWall", RpcTarget.Others);
       
    }


    [PunRPC]
    void RPC_DestroyWall()
    {
        puzzle1.GetComponent<Puzzle1>().DestroyWall();
        if (PuzzleManager.instance.pData.wallIsOpenPlayer1)
        {
            // set the bool to true in the other players puzzleData
            PuzzleManager.instance.pData.wallIsOpenPlayer2 = true;
        }
        else if (PuzzleManager.instance.pData.wallIsOpenPlayer2)
        {
            // set the bool to true in the other players puzzleData
            PuzzleManager.instance.pData.wallIsOpenPlayer1 = true;
        }
    }
   
    [PunRPC]
    void RPC_AllSolved()
    {
        PuzzleManager.instance.pData.hasOtherPlayerSolvedPuzzles = true;
        if (PuzzleManager.instance.pData.hasOtherPlayerSolvedPuzzles && PuzzleManager.instance.pData.allPuzzlesSolved)
        {
            PuzzleManager.instance.OpenFences();
        }

    }
    void CheckAll()
    {
        OtherSolvedPuzzles();
    }
}
