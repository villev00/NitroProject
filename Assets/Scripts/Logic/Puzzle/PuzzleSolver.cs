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
    }
    void Start()
    {   

        if(pv.IsMine)
        puzzle1 = GameObject.Find("Puzzle1_World" + PhotonNetwork.LocalPlayer.ActorNumber);
          
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;
        if (other.CompareTag("Puzzle1Cauldron"))
        {
            SolvePuzzle1();
        }
    }

    void SolvePuzzle1()
    {
        PuzzleManager.instance.pData.isSolved1 = true;
        PuzzleManager.instance.CheckPuzzle1();
        PuzzleManager.instance.CheckAllPuzzles();
        if (PuzzleManager.instance.pData.allPuzzlesSolved && pv.IsMine)
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
    }
   
    [PunRPC]
    void RPC_AllSolved()
    {
        PuzzleManager.instance.pData.hasOtherPlayerSolvedPuzzles = true;
        PuzzleManager.instance.CheckAllPuzzles();

    }
}
