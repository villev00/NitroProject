using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform bossSpawnPoint;
    [SerializeField]
    private GameObject bossHealthSlider;
    private bool hasSpawnedBoss = false;
    [SerializeField]
    private SettingsMenu menu;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasSpawnedBoss)
        {
            SpawnBoss();
            hasSpawnedBoss = true;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            bossHealthSlider.SetActive(true);
            menu.ChangeMusic();
        }
    }
    public void SpawnBoss()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FinalBoss"), bossSpawnPoint.transform.position, Quaternion.identity);
    }
}
