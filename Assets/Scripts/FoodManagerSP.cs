using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManagerSP : MonoBehaviour
{
    public GameObject foodPrefab;
    public GameObject oWallPrefab;
    public float spawnInterval = 5.0f;
    public Transform platform;
    public float xBounds;
    public float zBounds;
    public float minSpawnDistance = 1.5f;
    public float maxSpawnDistance = 3.0f;

    public GameObject collidedFood;
    private GameObject currentFood;
    private float spawnTimer = 0.0f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (currentFood == null && spawnTimer >= spawnInterval)
        {
            SpawnFood();
            spawnTimer = 0.0f;
            //collidedFood = currentFood;
        }
    }

    private void SpawnFood()
    {
        Vector3 position = Vector3.zero;

        while (position == Vector3.zero || Vector3.Distance(position, transform.position) < minSpawnDistance)
        {
            position = new Vector3(Random.Range(-xBounds, xBounds), 0, Random.Range(-zBounds, zBounds));
        }

        if ((position != foodPrefab.transform.position && position != foodPrefab.transform.position + new Vector3(10, 10, 10)) && (position != oWallPrefab.transform.position && position != foodPrefab.transform.position + new Vector3(10, 10, 10)))
        {
            currentFood = Instantiate(foodPrefab, position + new Vector3(0, 19, 0), Quaternion.identity);
            collidedFood = currentFood;
            currentFood.GetComponent<SnakeFoodSP>().type = "static";

            if (Random.value < 0.5f)
            {
                Vector3 direction = new Vector3(Random.Range(-xBounds, xBounds), 0f, Random.Range(-zBounds, zBounds)).normalized;
                currentFood.GetComponent<SnakeFoodSP>().type = "moving";
                currentFood.GetComponent<SnakeFoodSP>().direction = direction;
            }
        }

    }
}
