using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings(); //connects to photon master server

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Now connected to the " + PhotonNetwork.CloudRegion + " server!");
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;
       
            if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }
        if (currentName == "Replaced" && next.name == "Menu")
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.ConnectUsingSettings(); //connects to photon master server
        }
        Debug.Log("Scenes: " + currentName + ", " + next.name);
    }
}
