using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWallCollision : MonoBehaviour
{
    public GameObject gameManager;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("SnakeHead") || other.gameObject.name == "SnakeHead")
        {
            Debug.Log("you lose");
            
            other.gameObject.GetComponent<SnakeMovement>().enabled = false;
            other.gameObject.GetComponent<SnakeBodyControl>().enabled = false;
            gameManager.GetComponent<GameEndManager>().ActivatePanel(gameManager.GetComponent<GameEndManager>().endGamePanel.name);
            other.gameObject.GetComponent<SnakeBodyControl>().ShowScore();
            
        }
    }
}
