using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OWallManager : MonoBehaviour
{
    public GameObject pFoodPrefab;
    public GameObject foodPrefab;
    public GameObject oWallPrefab;    
    public float spawnIntervalP = 5.0f;
    public Transform platform;
    public float xBoundsPF;
    public float zBoundsPF;
    public float minSpawnDistance = 1.5f;
    public float maxSpawnDistance = 3.0f;

    public GameObject collidedWall;
    private GameObject currentWall;
    private float spawnTimer = 0.0f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        DestroyWall(spawnTimer);

        if (currentWall == null && spawnTimer >= spawnIntervalP)
        {
            
            SpawnWall();
            spawnTimer = 0.0f;
            collidedWall = currentWall;
        }
    }

    private void SpawnWall()
    {
        Vector3 position = Vector3.zero;

        while (position == Vector3.zero || Vector3.Distance(position, transform.position) < minSpawnDistance)
        {
            position = new Vector3(Random.Range(-xBoundsPF, xBoundsPF), 0, Random.Range(-zBoundsPF, zBoundsPF));
        }

        if ((position != foodPrefab.transform.position && position != foodPrefab.transform.position + new Vector3(10, 10, 10)) && (position != pFoodPrefab.transform.position && position != pFoodPrefab.transform.position + new Vector3(10, 10, 10)))
        {
            float[] angles = new float[5] { 0.0f, 90.0f, 180.0f, 270.0f, 360.0f};
            currentWall = Instantiate(oWallPrefab, position + new Vector3(0, 48, 0), Quaternion.Euler(0, angles[Random.Range(0, 4)], 0));
            collidedWall = currentWall;
        }

    }

    private void DestroyWall(float spawnTimer)
    {
        if(spawnTimer >= 20.0f )
        Destroy(collidedWall);
    }
}
