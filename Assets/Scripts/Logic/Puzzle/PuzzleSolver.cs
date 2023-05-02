using Data;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    PhotonView pv;
    GameObject puzzle1;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (!pv.IsMine) return;
        int index = PhotonNetwork.LocalPlayer.ActorNumber;
        if (index > 2) index = 2;
        puzzle1 = GameObject.Find("Puzzle1_World" + index);
        PuzzleManager.instance.player = gameObject;
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
    void RPC_Platform()
    {       
        PuzzleManager.instance.CheckPlatform();
    }

    [PunRPC]
    void RPC_DestroyWall()
    {
        puzzle1.GetComponent<Puzzle1>().DestroyWall();
        pv.RPC("RPC_Platform", RpcTarget.All);

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
