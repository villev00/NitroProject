using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    private NavMeshAgent meleeEnemy;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        meleeEnemy= GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        meleeEnemy.SetDestination(player.position);
    }
}
