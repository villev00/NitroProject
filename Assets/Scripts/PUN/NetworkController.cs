using Photon.Pun;
using Photon.Realtime;
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
        Time.timeScale = 1;
        if(PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
        else
            PhotonNetwork.ConnectUsingSettings(); //connects to photon master server

        PhotonNetwork.AddCallbackTarget(this);
    }
 
    public override void OnConnectedToMaster()
    {
        Debug.Log("Now connected to the " + PhotonNetwork.CloudRegion + " server!");
        startGame.interactable= true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
       Debug.Log("Disconnected");
       PhotonNetwork.ConnectUsingSettings();
       base.OnDisconnected(cause);
    }
}
