using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrowdManager : MonoBehaviour
{
    public static CrowdManager instance;
    public CrowdSpawner[] spawners;

    public float spawnDelay = 10f;
    private float spawnTimer;
    public TopUpMachineController[] topUpMachinesGroup1;
    public TopUpMachineController[] topUpMachinesGroup2;
    public Transform[] targetDestinations;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        spawnTimer = 1f;

    
        
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            foreach(CrowdSpawner spawner in spawners)
            {
                spawner.SpawnAgent();
                spawnTimer = spawnDelay;
            }
        }
    }

    public Transform GetRandomDestination()
    {
        return targetDestinations[Random.Range(0, targetDestinations.Length)];
    }

 
}
