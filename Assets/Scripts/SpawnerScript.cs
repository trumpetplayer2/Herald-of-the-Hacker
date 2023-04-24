using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    bool isBlocked;
    float nextSpawn=2f;
    public float spawnTime = 5;
    public GameObject spawnedObject;
    public Transform spawnLocation;
    public bool isTemp = false;
    public float destroyTime = 10f;
    float destructionTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (isBlocked) { return; }
        if (nextSpawn <= 0)
        {
            if(spawnedObject != null)
            {
                Instantiate(spawnedObject, spawnLocation.position, Quaternion.identity);
                nextSpawn = spawnTime;
            }
        }
        else
        {
            nextSpawn -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isBlocked = true;
        if (isTemp)
        {
            destructionTime += Time.deltaTime;
            if(destructionTime > destroyTime)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isBlocked = false;
        nextSpawn = spawnTime;
    }
}
