using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePoisonFoodSP : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("SnakeHead") || other.gameObject.name == "SnakeHead")
        {
            Debug.Log("Colliding");
            Destroy(gameObject);
            other.GetComponent<SnakeBodyControlSP>().ShrinkSnake();
            if (other.GetComponent<SnakeBodyControlSP>().bodyPartsCount == 0)
            {
                other.gameObject.GetComponent<SnakeMovementSP>().enabled = false;
                other.gameObject.GetComponent<SnakeBodyControlSP>().enabled = false;

            }
        }
    }
}
