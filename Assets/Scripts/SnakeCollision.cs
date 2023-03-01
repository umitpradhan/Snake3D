//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;
//using Photon.Realtime;

//public class SnakeCollision : MonoBehaviourPun
//{
//    public bool isPlayer1 = true; // Set to true for player 1, false for player 2 in multiplayer mode

//    private void OnTriggerEnter(Collider other)
//    {
//        if(photonView.IsMine)
//        {
//            if (other.CompareTag("SnakeHead") || other.CompareTag("Tail"))
//            {
//                // Snake collided with another player's snake head
//                if (isPlayer1 != other.GetComponent<SnakeCollision>().isPlayer1)
//                {
//                    Time.timeScale = 0;
//                }
//            }
//        }
        
//    }
//}
