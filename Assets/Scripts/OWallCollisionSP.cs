using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWallCollisionSP : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("SnakeHead") || other.gameObject.name == "SnakeHead")
        {
            Debug.Log("you lose");
            Destroy(gameObject);
            other.gameObject.GetComponent<SnakeMovementSP>().enabled = false;
            other.gameObject.GetComponent<SnakeBodyControlSP>().enabled = false;
            
        }
    }
}
