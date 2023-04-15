using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform bossSpawnPoint;

    private bool hasSpawnedBoss = false;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasSpawnedBoss)
        {
            SpawnBoss();
            hasSpawnedBoss = true;
        }
    }
    public void SpawnBoss()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Boss"), bossSpawnPoint.transform.position, Quaternion.identity);
    }
}
