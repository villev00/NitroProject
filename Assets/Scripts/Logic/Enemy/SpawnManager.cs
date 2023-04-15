using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // singleton
    public static SpawnManager instance;  
    [SerializeField] public SpawnData spawnData;

    private void Start()
    {
        instance = this;
        spawnData = new SpawnData();


    }
    
}
