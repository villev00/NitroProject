using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject startButton;

    [SerializeField]
    int roomSize;

    public override void OnConnectedToMaster() //when first connection is established
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.GetComponent<Button>().onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        startButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Loading...";
        startButton.GetComponent<Button>().interactable = false;
        if (PhotonNetwork.IsConnectedAndReady) // check if the player is currently connected to the PhotonNetwork and ready to perform operations
            PhotonNetwork.JoinRandomRoom(); // attempt to join a random room
        
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);
        SceneManager.LoadScene("Lobby");
    }
  
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    void CreateRoom() //trying to create a new room
    {
        Debug.Log("Creating room");
        int randomRoomNumber = Random.Range(0, 10000); //random name for room
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps); //attempting to create a new room with these settings
        Debug.Log(randomRoomNumber);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room.. trying again");
        CreateRoom(); //retrying to create room with different name
    }

}
