using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SnakeController : MonoBehaviour
{
    public GameObject tailPrefab;
    //public GameObject headPrefab;
    //GameObject foodRefPos;
    public Text scoreText;
    public int scorePerFood = 10;
    //public float minDis =1f;
    //public float rotSpeed = 50.0f;
    //public float speed = 1.0f;

    public float BodySpeed = 5;
    public int Gap = 10;

    //private List<Transform> tailTransforms = new List<Transform>();
    //private Vector3 lastTailPosition;
    //private Transform currTail;
    //private Transform prevTail;
    //private float dis;
    private int score = 0;
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    //private void Start()
    //{
    //    currTail = transform;
    //    currTail.position = transform.position - new Vector3(1, 0, 0);
    //}

    private void Update()
    {

        PositionsHistory.Insert(0, transform.position);

        // Move body parts
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;

            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);

            index++;
        }

        //float distance = 50.0f;
        //if (GameObject.FindGameObjectWithTag("SnakeFood")) 
        //{
        //    foodRefPos = GameObject.FindGameObjectWithTag("SnakeFood");
        //}

        ////currTail = transform;
        //currTail.position = transform.position ;
        //// Move the tail
        //if (tailTransforms.Count > 0)
        //{
        //    tailTransforms[0].position = transform.position;
        //    //tailTransforms[0].position = (tailTransforms[0].position - transform.position).normalized * distance + transform.position;
        //    for (int i = 1; i < tailTransforms.Count; i++)
        //    {
        //        Debug.Log(tailTransforms[0] );
        //        Debug.Log(i);
        //        currTail = tailTransforms[i];
        //        Debug.Log(currTail + "currTail");Debug.Log(currTail.position + "currTail");
        //        prevTail = tailTransforms[i - 1];
        //        Debug.Log(prevTail + "prevTail");Debug.Log(prevTail.position + "prevTail");
        //        //prevTail.position = currTail.position;
        //        //currTail.position = tailTransforms[i].position;
        //        //prevTail.position = currTail.position;


        //        // Calculate the distance between the previous tail segment and the current tail segment
        //        float prevSegmentDist = Vector3.Distance(prevTail.position, currTail.position);
        //        Debug.Log(prevSegmentDist + "prevsegDist");
        //        // Calculate the position the current tail segment should be in to maintain the minimum distance between tail segments
        //        Vector3 newpos = prevTail.position + ((currTail.position - prevTail.position).normalized * (prevSegmentDist - minDis));
        //        Debug.Log(newpos + "newpos");
        //        // Move the current tail segment towards its new position
        //        float T = Time.deltaTime * speed;
        //        currTail.position = Vector3.Lerp(currTail.position, newpos, T);
        //        Debug.Log(currTail.position + "currTailpos");
        //        //currTail.rotation = Quaternion.Slerp(currTail.rotation, prevTail.rotation, T);
        //        //Debug.Log(currTail.rotation + "currTailrot");




















        //        //dis = Vector3.Distance(prevTail.position, currTail.position);

        //        //Vector3 newpos = prevTail.position;

        //        //newpos.y = tailTransforms[0].position.y;

        //        //float T = Time.deltaTime * dis / minDis * speed;

        //        //if (T > 0.5f)
        //        //    T = 0.5f;
        //        //currTail.position = Vector3.Slerp(currTail.position, newpos, T);
        //        //currTail.rotation = Quaternion.Slerp(currTail.rotation, prevTail.rotation, T);
























        //        //tailTransforms[i].position = transform.TransformPoint(0, 0, -1);
        //        //Vector3 prevTailPosition = tailTransforms[i].TransformPoint(0, 0, -1);
        //        //tailTransforms[i].position = lastTailPosition;
        //        //lastTailPosition = prevTailPosition;
        //        //tailTransforms[i].position = Vector3.Lerp(tailTransforms[i].position, tailTransforms[i-1].position,headPrefab.GetComponent<SnakeMovement>().moveSpeed*3*Time.deltaTime);
        //        //tailTransforms[i].position = (tailTransforms[i].position - tailTransforms[i-1].position).normalized * distance + tailTransforms[i - 1].position;
        //    }

        //}
        ////if (tailTransforms.Count > 0)
        ////{
        ////    // Move the last tail segment to the position of the head
        ////    tailTransforms[tailTransforms.Count - 1].position = transform.TransformPoint(0, 0, -1);

        ////    // Move the other tail segments to the position of the tail segment in front of them
        ////    for (int i = tailTransforms.Count - 2; i >= 0; i--)
        ////    {
        ////        tailTransforms[i].position = tailTransforms[i + 1].TransformPoint(0, 0, -1);
        ////    }

        ////    // Update the last tail position to the position of the second to last tail segment
        ////    lastTailPosition = tailTransforms[tailTransforms.Count - 2].position;
        ////}

    }

    public void AddTail()
    {
        GameObject body = Instantiate(tailPrefab);
        BodyParts.Add(body);

        score += scorePerFood;
        //scoreText.text = "Score: " + score;












        //GameObject newTail;
        //= Instantiate(tailPrefab, tailTransforms[tailTransforms.Count - 1].position, tailTransforms[tailTransforms.Count - 1].rotation);
        //newTail.transform.parent = transform;


        //Vector3 headpos= transform.position;

        //Vector3 headDir= transform.forward;

        //float distance = 10.0f;
        //Vector3 headDirVector = headDir * distance;

        //Vector3 posTail = headpos+ headDirVector;













        ////Vector3 lastTailLocalPosition= lastTailPosition - transform.position;
        //////GameObject newTail = Instantiate(tailPrefab, lastTailPosition, Quaternion.identity);
        //////newTail.transform.position = transform.position + lastTailLocalPosition.normalized * -1.0f;
        //GameObject newTail1;
        //GameObject newTail2;

        ////if (tailTransforms.Count == 0)
        ////{
        ////    newTail = Instantiate(tailPrefab, lastTailPosition, Quaternion.identity);
        ////    newTail.transform.position = transform.position + lastTailLocalPosition.normalized * -1.0f;
        ////}
        ////else
        ////{
        ////    newTail = Instantiate(tailPrefab, lastTailPosition, tailTransforms[tailTransforms.Count - 1].rotation);
        ////}

        ////tailTransforms.Add(newTail.transform);
        ////lastTailPosition = newTail.transform.position;
        //// If the snake doesn't have any tail segment, attach the new tail to the head



        ////if (tailTransforms.Count == 0)
        ////{
        ////    GameObject newTail = Instantiate(tailPrefab, lastTailPosition, Quaternion.identity);
        ////    tailTransforms.Add(newTail.transform);
        ////    lastTailPosition = newTail.transform.position;
        ////}
        ////else
        ////{
        ////    // Attach the new tail to the last tail segment
        ////    Vector3 lastTailLocalPosition = lastTailPosition - tailTransforms[tailTransforms.Count - 1].position;
        ////    GameObject newTail = Instantiate(tailPrefab, tailTransforms[tailTransforms.Count - 1].position, Quaternion.identity);
        ////    newTail.transform.position = tailTransforms[tailTransforms.Count - 1].position + lastTailLocalPosition.normalized * -1.0f;
        ////    tailTransforms.Add(newTail.transform);
        ////    lastTailPosition = newTail.transform.position;
        ////}

        //GameObject newTail;


        //// If the snake doesn't have any tail segments, spawn a new one directly behind the head
        //if (tailTransforms.Count == 0)
        //{
        //    Vector3 headpos = transform.position;

        //    Vector3 headDir = transform.forward;

        //    float distance = 100.0f;
        //    Vector3 headDirVector = headDir * distance;

        //    Vector3 posTail = headpos + headDirVector;
        //    //newTail = Instantiate(tailPrefab, transform.position - (transform.forward * 200), Quaternion.identity);
        //    newTail1 = Instantiate(tailPrefab, posTail - new Vector3(0,0,-30), Quaternion.identity);
        //    newTail1.transform.parent = transform;
        //    Debug.Log(transform.position - transform.forward);
        //    Debug.Log(posTail+ "pstail");
        //    Debug.Log(newTail1.transform.rotation + "newtailRot");
        //    Debug.Log(transform.forward * 20);
        //    //tailTransforms.Add(newTail1.transform);
        //    //newTail.transform.parent = transform;

        //}
        //else
        //{
        //    // Otherwise, spawn a new tail segment at the position of the last tail segment
        //    //newTail = Instantiate(tailPrefab, lastTailPosition - tailTransforms[tailTransforms.Count - 1].forward, Quaternion.identity);
        //    //newTail = Instantiate(tailPrefab, lastTailPosition - foodM.GetComponent<FoodManager>().collidedFood.transform.position , Quaternion.identity);
        //    //newTail = Instantiate(tailPrefab, foodRefPos.transform.position - tailTransforms[tailTransforms.Count - 1].forward, Quaternion.LookRotation(headPrefab.GetComponent<SnakeMovement>().moveDirection));
        //    newTail2 = Instantiate(tailPrefab, tailTransforms[tailTransforms.Count - 1].position - new Vector3(0, 0, -1), Quaternion.identity);
        //    newTail2.transform.parent = transform;
        //    tailTransforms.Add(newTail2.transform);

        //}

        //tailTransforms.Add(newTail.transform);
        //lastTailPosition = tailTransforms[tailTransforms.Count - 1].position;








    }
}
