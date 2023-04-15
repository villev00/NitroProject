using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    [SerializeField] GameObject[] spawnLocation; 
    int playerIndex;
    void Start()
    {
        playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;
        CreatePlayer();
        if(PhotonNetwork.IsMasterClient)
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Boss"), spawnLocation[0].transform.position, Quaternion.identity);
    }

    void CreatePlayer()
    {
        Debug.Log("Creating player");
     //   PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"+playerIndex), spawnLocation[playerIndex - 1].transform.position, Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"+playerIndex), new Vector3(playerIndex * 2, 0), Quaternion.identity);
    }
}
