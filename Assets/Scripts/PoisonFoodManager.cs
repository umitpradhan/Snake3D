using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoisonFoodManager : MonoBehaviour
{
    public GameObject pFoodPrefab;
    public GameObject foodPrefab;
    public GameObject oWallPrefab;
    public GameObject hSnakePrefab;
    public float spawnIntervalP = 5.0f;
    public Transform platform;
    public float xBoundsPF;
    public float zBoundsPF;
    public float minSpawnDistance = 1.5f;
    public float maxSpawnDistance = 3.0f;

    public GameObject collidedPFood;
    private GameObject currentPFood;
    private float spawnTimer = 0.0f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (currentPFood == null && spawnTimer >= spawnIntervalP)
        {
            SpawnFood();
            spawnTimer = 0.0f;
            collidedPFood = currentPFood;
        }
    }

    private void SpawnFood()
    {
        Vector3 position = Vector3.zero;

        while (position == Vector3.zero || Vector3.Distance(position, transform.position) < minSpawnDistance)
        {
            position = new Vector3(Random.Range(-xBoundsPF, xBoundsPF), 0, Random.Range(-zBoundsPF, zBoundsPF));
        }

        if ((position != foodPrefab.transform.position && position != foodPrefab.transform.position + new Vector3(10,10,10)) && (position != oWallPrefab.transform.position && position != foodPrefab.transform.position + new Vector3(10, 10, 10)))
        {
            currentPFood = Instantiate(pFoodPrefab, position + new Vector3(0, 25, 0), Quaternion.identity);
            collidedPFood = currentPFood;
            
                          
        }

    }
}
