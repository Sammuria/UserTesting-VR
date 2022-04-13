using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSpawner : MonoBehaviour
{
    public GameObject[] characters;
    public float range = 5;
    public int spawnGroup;
    public Vector3 GetRandomOffset()
    {
        return new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
    }

    public void SpawnAgent()
    {
        GameObject spawnedAgent = Instantiate(characters[Random.Range(0, characters.Length)], transform.position + GetRandomOffset(), transform.rotation);
        spawnedAgent.GetComponent<MovingAgentController>().spawnGroup = spawnGroup;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnAgent();
        }
    }
}
