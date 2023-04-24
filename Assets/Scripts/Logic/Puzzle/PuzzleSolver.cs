using Data;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    PhotonView pv;
    public GameObject puzzle1;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
     //   InvokeRepeating("WallCheck", 5, 5);
     //   InvokeRepeating("BothPlayersOnPlatform", 5, 5);
    }

    void Start()
    {
        if (!pv.IsMine) return;
        int index = PhotonNetwork.LocalPlayer.ActorNumber;
        if (index > 2) index = 2;
        puzzle1 = GameObject.Find("Puzzle1_World" + index);
        PuzzleManager.instance.player = gameObject;
    }

    [PunRPC]
    void BothPlayersOnPlatform()
    {
        if (PuzzleManager.instance.pData.playerStanding &&
            PuzzleManager.instance.pData.otherPlayerStanding)
        {
            PuzzleManager.instance.pData.bothPlayersStanding = true;
        }
    }
    public void otherPlayerStanding()
    {
        if (pv.IsMine)
        {
            if (PuzzleManager.instance.pData.playerStanding &&
            PuzzleManager.instance.pData.otherPlayerStanding)
            {
                PuzzleManager.instance.pData.bothPlayersStanding = true;
            }
            gameObject.GetComponent<PlayerLogic>().otherPlayer.GetComponent<PhotonView>()
                 .RPC("RPC_OtherStanding", RpcTarget.Others);
            pv.RPC("BothPlayersOnPlatform", RpcTarget.Others);
        }
    }
    [PunRPC]
    void RPC_OtherStanding()
    {
        PuzzleManager.instance.pData.otherPlayerStanding = true;
        puzzle1.gameObject.GetComponent<Puzzle1>().otherPlayerLights();
       
       
    }
   
    public void OtherSolvedPuzzles()
    {
        if (PuzzleManager.instance.pData.allPuzzlesSolved)
            gameObject.GetComponent<PlayerLogic>().otherPlayer.GetComponent<PhotonView>()
                .RPC("RPC_AllSolved", RpcTarget.Others);
    }
    
    public void DestroyWall()
    {
        if (pv.IsMine)
        {
            gameObject.GetComponent<PlayerLogic>().otherPlayer.GetComponent<PhotonView>()
                .RPC("RPC_DestroyWall", RpcTarget.Others);
            PuzzleManager.instance.pData.wallWasDestroyed = true;
        }     
    }


    [PunRPC]
    void RPC_DestroyWall()
    {
        puzzle1.GetComponent<Puzzle1>().DestroyWall();
    }
        
    [PunRPC]
    void RPC_AllSolved()
    {
        PuzzleManager.instance.pData.hasOtherPlayerSolvedPuzzles = true;
        if (PuzzleManager.instance.pData.hasOtherPlayerSolvedPuzzles &&
            PuzzleManager.instance.pData.allPuzzlesSolved)
        {
            if (pv.IsMine)
                pv.RPC("RPC_OpenFence", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPC_OpenFence()
    {
        PuzzleManager.instance.OpenFences();
    }
}
