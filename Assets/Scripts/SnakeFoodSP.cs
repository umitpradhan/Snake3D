using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFoodSP : MonoBehaviour
{

    public string type;
    public Vector3 direction;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("SnakeHead") || other.gameObject.name == "SnakeHead")
        {
            Debug.Log("Colliding");
            Destroy(gameObject);
            other.GetComponent<SnakeBodyControlSP>().GrowSnake();
        }
    }
}
