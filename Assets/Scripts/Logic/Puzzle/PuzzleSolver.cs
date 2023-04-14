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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SolvePuzzle1();
        }
    }
    public void SolvePuzzle1()
    {
        if (pv.IsMine)
            gameObject.GetComponent<PlayerLogic>().otherPlayer.GetComponent<PhotonView>().RPC("RPC_SolvePuzzle1", RpcTarget.Others);
        PuzzleManager.instance.CheckPuzzle1();
    }


    [PunRPC]
    void RPC_SolvePuzzle1()
    {
        puzzle1.GetComponent<Puzzle1>().DestroyWall();

    }
}
