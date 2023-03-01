using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakePoisonFood : MonoBehaviour
{
    public GameObject gameManager;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("SnakeHead") || other.gameObject.name == "SnakeHead")
        {
            Debug.Log("Colliding");
            
            other.GetComponent<SnakeBodyControl>().ShrinkSnake();
            if(other.GetComponent<SnakeBodyControl>().bodyPartsCount == 0)
            {
                other.gameObject.GetComponent<SnakeMovement>().enabled = false;
                other.gameObject.GetComponent<SnakeBodyControl>().enabled = false;
                
                
                    gameManager.GetComponent<GameEndManagerSP>().ActivatePanel(gameManager.GetComponent<GameEndManagerSP>().endGamePanel.name);
                    other.gameObject.GetComponent<SnakeBodyControl>().ShowScore();

                other.gameObject.GetComponent<SnakeBodyControl>().ShowScore();
            }
        }
    }
}

