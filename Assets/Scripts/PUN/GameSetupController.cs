using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    [SerializeField] GameObject[] spawnLocation; 
    int locationIndex;
    void Start()
    {
        locationIndex = PhotonNetwork.LocalPlayer.ActorNumber;
        CreatePlayer();
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Boss"), spawnLocation[0].transform.position, Quaternion.identity);
    }

    void CreatePlayer()
    {
        Debug.Log("Creating player");
     //   PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), spawnLocation[locationIndex - 1].transform.position, Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), new Vector3(locationIndex * 2, 0), Quaternion.identity);
    }
}
