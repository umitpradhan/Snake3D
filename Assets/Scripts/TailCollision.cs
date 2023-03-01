using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.SceneManagement;

public class TailCollision : MonoBehaviour
{
    public GameObject gameManager;
    void OnTriggerEnter(Collider other)
    {
        
        
            if (other.CompareTag("Tail"))
            {
            //gameObject.GetComponent<SnakeBodyControl>().dBodyParts.Clear();
            //for (int i = gameObject.GetComponent<SnakeBodyControl>().bodyPartsCount; i>0; i--)
            //{
            //    gameObject.GetComponent<SnakeBodyControl>().dBodyParts.Clear();
            //    GameObject dBody = gameObject.GetComponent<SnakeBodyControl>().dBodyParts[i];
            //    Destroy(dBody);
            //}
            //Destroy(gameObject);
            
            gameObject.GetComponent<SnakeMovement>().enabled = false;
            gameObject.GetComponent<SnakeBodyControl>().enabled = false;
            if (SceneManager.GetActiveScene().name== "MultiPlayerModeScene")
            {
                gameManager.GetComponent<GameEndManagerSP>().ActivatePanel(gameManager.GetComponent<GameEndManagerSP>().endGamePanel.name);
                other.gameObject.GetComponent<SnakeBodyControl>().ShowScore();

            }
            else if(SceneManager.GetActiveScene().name == "SinglePlayerModeScene")
            {
                gameManager.GetComponent<GameEndManager>().ActivatePanel(gameManager.GetComponent<GameEndManager>().endGamePanel.name);
                other.gameObject.GetComponent<SnakeBodyControlSP>().ShowScoreSP();
            }
            
            
        }
            Debug.Log(ReturnDirection(other.gameObject, this.gameObject));
        
        
    }

    private enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right }
    private HitDirection ReturnDirection(GameObject Object, GameObject ObjectHit)
    {

        HitDirection hitDirection = HitDirection.None;
        RaycastHit MyRayHit;
        Vector3 direction = (Object.transform.position - ObjectHit.transform.position).normalized;
        Ray MyRay = new Ray(ObjectHit.transform.position, direction);

        if (Physics.Raycast(MyRay, out MyRayHit))
        {

            if (MyRayHit.collider != null)
            {

                Vector3 MyNormal = MyRayHit.normal;
                MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                //if (MyNormal == MyRayHit.transform.up) { hitDirection = HitDirection.Top; }
                //if (MyNormal == -MyRayHit.transform.up) { hitDirection = HitDirection.Bottom; }
                //if (MyNormal == MyRayHit.transform.forward) { hitDirection = HitDirection.Forward; }
                //if (MyNormal == -MyRayHit.transform.forward) { hitDirection = HitDirection.Back; }
                if (MyNormal == MyRayHit.transform.right) { hitDirection = HitDirection.Right; }
                if (MyNormal == -MyRayHit.transform.right) { hitDirection = HitDirection.Left; }
            }
        }
        return hitDirection;
    }
}

