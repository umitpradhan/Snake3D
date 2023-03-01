using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SocialPlatforms.Impl;

public class SnakeBodyControlSP : MonoBehaviour
{
    // Settings
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float rotSpeed = 10;
    public float BodySpeed = 5;
    public float masterCount;
    public float otherCount;
    public int Gap = 10;
    Vector3 moveDirection;
    public Text scoreText;
    public Text totalScore;
    
    public int scorePerFood = 10;
    public bool isGrow;


    // References
    public GameObject BodyPrefab;
    public List<GameObject> dBodyParts;
    public int bodyPartsCount;
    private int score = 0;
    private GameObject scoreContainer;
    private GameObject scoreContainer1;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        scoreContainer = GameObject.FindGameObjectWithTag("Score");
        scoreText = scoreContainer.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
            bodyPartsCount = BodyParts.Count;

            // Store position history
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
        
    }

    public void GrowSnake()
    {
        // Instantiate body instance and
        // add it to the list
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
        //dBodyParts.Add(body);
        score += scorePerFood;
        string s = score.ToString();
        scoreText.text = s;
        gameObject.GetComponent < SnakeMovementSP>().moveSpeed += (scorePerFood / 2);
    }

    public void ShrinkSnake()
    {
        if (bodyPartsCount > 0)
        {
            GameObject dBody = BodyParts[bodyPartsCount - 1];            
            Destroy(dBody);
            BodyParts.RemoveAt(BodyParts.Count - 1);
            score -= scorePerFood;
            string s = score.ToString();
            scoreText.text = s;
            gameObject.GetComponent<SnakeMovementSP>().moveSpeed -= (scorePerFood / 5);
        }
    }


    public void ShowScoreSP()
    {
        
            
                totalScore = scoreText;
         
       
 

    }
}
