using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] Button startGame;
    private void Start()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings(); //connects to photon master server
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Now connected to the " + PhotonNetwork.CloudRegion + " server!");
        startGame.interactable= true;
    }

  
}
