using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject roomPanel, lobbyPanel, startGame, dif;

    [SerializeField] TextMeshProUGUI playerCount;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public override void OnJoinedRoom()
    {
        roomPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        playerCount.text = "Players in Room: \n" + PhotonNetwork.CurrentRoom.PlayerCount;
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        playerCount.text = "Players in Room: \n" + PhotonNetwork.CurrentRoom.PlayerCount;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            startGame.SetActive(true); 
            dif.SetActive(true);
        }

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        playerCount.text = "Players in Room: \n" + PhotonNetwork.CurrentRoom.PlayerCount;
        if (PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            startGame.SetActive(false);
        }
      
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.LeaveLobby();
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
      
    }
}