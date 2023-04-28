using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static LobbyController lobby;


    public string roomName;
    [SerializeField] int roomSize;

    public GameObject roomListingPrefab;
    public Transform roomsPanel;

    private void Awake()
    {
        lobby = this;
    }
    public override void OnConnectedToMaster() //when first connection is established
    {
        PhotonNetwork.AutomaticallySyncScene = true;
      
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        if(roomsPanel.gameObject.activeInHierarchy) return;
        RemoveRoomListings();

        foreach (RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }
    void ListRoom(RoomInfo room)
    {
        if(room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();

            tempButton.roomName = room.Name;
            tempButton.roomSize = room.MaxPlayers;
            tempButton.SetRoom();
        }
    }
    void RemoveRoomListings()
    {
        for(int i=0; i<roomsPanel.childCount; i++)
        {
            Destroy(roomsPanel.GetChild(i).gameObject);
        }
    }

    public void StartGame()
    {
      //  roomsPanel.
      SceneManager.LoadScene("Level1");
    }
    public void CreateRoom() //trying to create a new room
    {
        Debug.Log("Creating room");
 
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOps); //attempting to create a new room with these settings
        
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room.. trying again");
        //name already chosen, choose another room name
    }

    public void OnRoomNameChanged(string nameIn)
    {
        if (nameIn.Length > 10)
            roomName = nameIn.Substring(0, Mathf.Min(name.Length, 10));
        else
            roomName = nameIn;
    }

    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        else
        {
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.JoinLobby();
        }
        if (roomsPanel.gameObject.activeSelf) roomsPanel.gameObject.SetActive(false);
        else roomsPanel.gameObject.SetActive(true);
    }

}
