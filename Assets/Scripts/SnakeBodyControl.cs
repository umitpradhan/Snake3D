using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SocialPlatforms.Impl;

public class SnakeBodyControl : MonoBehaviourPun
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
    public Text scoreText1;
    public int scorePerFood = 10;
    public bool isGrow;
    public Text totalScore;


    // References
    public GameObject BodyPrefab;
    public List<GameObject> dBodyParts;
    public int bodyPartsCount;
    private int score = 0;
    private int score1 = 0;
    private GameObject scoreContainer;
    private GameObject scoreContainer1;
    

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    //[PunRPC]
    //void UpdateScores(float masterScore, float otherScore)
    //{
    //     masterCount = masterScore;
    //     otherCount = otherScore;

    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        scoreContainer = GameObject.FindGameObjectWithTag("Score");
    //        scoreText = scoreContainer.GetComponent<Text>(); 
    //        scoreText.text = Mathf.Floor(masterCount).ToString("0");
    //    }
    //    else
    //    {
    //        scoreContainer1 = GameObject.FindGameObjectWithTag("Score1");
    //        scoreText = scoreContainer1.GetComponent<Text>();
    //        scoreText.text = Mathf.Floor(otherCount).ToString("0");
    //    }
    //}
    //[PunRPC]
    //void MasterCount(int count)
    //{
    //    //scoreContainer = GameObject.FindGameObjectWithTag("Score");
    //    //scoreText = scoreContainer.GetComponent<Text>();
    //    masterCount = count;
    //}

    //[PunRPC]
    //void OtherCount(int count)
    //{
    //    //scoreContainer1 = GameObject.FindGameObjectWithTag("Score1");
    //    //scoreText = scoreContainer1.GetComponent<Text>();
    //    otherCount = count;
    //}

    void Start()
    {
        //GrowSnake();
        //GrowSnake();
        //GrowSnake();
        //GrowSnake();
        //GrowSnake();
        if (PhotonNetwork.IsMasterClient)
        {
            if(photonView.IsMine)
            {
                scoreContainer = GameObject.FindGameObjectWithTag("Score");
                scoreText = scoreContainer.GetComponent<Text>();
                //scoreText.text = masterCount.ToString();
            //}
            //else
            //{
                scoreContainer1 = GameObject.FindGameObjectWithTag("Score1");
                scoreText1 = scoreContainer1.GetComponent<Text>();
                //scoreText.text = otherCount.ToString();
            }

        }
        else
        {
            if (photonView.IsMine)
            {
                scoreContainer1 = GameObject.FindGameObjectWithTag("Score1");
                scoreText1 = scoreContainer1.GetComponent<Text>();
                //scoreText.text = otherCount.ToString();   
            //}
            //else
            //{
                scoreContainer = GameObject.FindGameObjectWithTag("Score");
                scoreText = scoreContainer.GetComponent<Text>();
                //scoreText.text = masterCount.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (photonView.IsMine)
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
    }

    
    public void GrowSnake()
    {
        //// Instantiate body instance and
        //// add it to the list
        //GameObject body = Instantiate(BodyPrefab);
        //BodyParts.Add(body);
        ////dBodyParts.Add(body);
        ////GrowSnakeS();
        //gameObject.GetComponent<PhotonView>().RPC("GrowSnakeScore", RpcTarget.Others);

        // Instantiate body instance and
        // add it to the list
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
        //dBodyParts.Add(body);
        if (PhotonNetwork.IsMasterClient)
        {
            if (photonView.IsMine)
            {
                score += scorePerFood;
                scoreText.text = score.ToString();
                //scoreContainer1.SetActive(false);
            }
            else
            {
                score1 += scorePerFood;
                scoreText1.text = score1.ToString();
            }
            
        }
        else
        {
            if (photonView.IsMine)
            {
                score1 += scorePerFood;
                scoreText1.text = score1.ToString();
                //scoreContainer.SetActive(false);
            }
            else
            {
                score += scorePerFood;
                scoreText.text = score.ToString();
            }
        }
        gameObject.GetComponent<SnakeMovement>().moveSpeed += (scorePerFood / 2);
        //score += scorePerFood;
        //scoreText.text = score.ToString();

        //if (!PhotonNetwork.IsMasterClient)
        //{// Add the score for the second player
        //    score1 += scorePerFood;
        //    scoreText1.text = score1.ToString();
        //}
        // Synchronize the score across the network
        PhotonView.Get(this).RPC("UpdateScore", RpcTarget.All, score, score1);

    }

    public void ShrinkSnake()
    {
        //if (bodyPartsCount > 0)
        //{
        //    GameObject dBody = BodyParts[bodyPartsCount - 1];
        //    BodyParts.RemoveAt(BodyParts.Count - 1);
        //    Destroy(dBody);            
        //    //ShrinkSnakeS();
        //    gameObject.GetComponent<PhotonView>().RPC("ShrinkSnakeScore", RpcTarget.Others);
        //}

        // Instantiate body instance and
        // add it to the list
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
        //dBodyParts.Add(body);
        if (PhotonNetwork.IsMasterClient)
        {
            if (photonView.IsMine)
            {
                score -= scorePerFood;
                scoreText.text = score.ToString();
                //scoreContainer1.SetActive(false);
            }
            else
            {
                score1 -= scorePerFood;
                scoreText1.text = score1.ToString();
            }
        }
        else
        {
            if (photonView.IsMine)
            {
                score1 -= scorePerFood;
                scoreText1.text = score1.ToString();
                //scoreContainer.SetActive(false);
            }
            else
            {
                score -= scorePerFood;
                scoreText.text = score.ToString();
            }
        }
        gameObject.GetComponent<SnakeMovement>().moveSpeed -= (scorePerFood / 5);
        //    score -= scorePerFood;
        //    scoreText.text = score.ToString();

        //if (!PhotonNetwork.IsMasterClient)
        //{    // Add the score for the second player
        //    score1 -= scorePerFood;
        //    scoreText1.text = score1.ToString();
        //}
        // Synchronize the score across the network
        PhotonView.Get(this).RPC("UpdateScore", RpcTarget.Others, score, score1);

    }

    [PunRPC]
    void UpdateScore(int score, int score1)
    {
        //this.score = score;
        //this.score1 = score1;
        //scoreText.text = score.ToString();
        //scoreText1.text = score1.ToString();

        if (PhotonNetwork.IsMasterClient)
        {
            if (photonView.IsMine)
            {
                this.score = score;
                scoreText.text = score.ToString();
            }
            else
            {
                this.score1 = score1;
                scoreText1.text = score1.ToString();
            }
        }
        else
        {
            if (photonView.IsMine)
            {
                this.score1 = score1;
                scoreText1.text = score1.ToString();
            }
            else
            {
                this.score = score;
                scoreText.text = score.ToString();
            }
        }
    }


    //[PunRPC]
    //public void GrowSnakeScore()
    //{


    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        if (photonView.IsMine)
    //        {
    //            score += scorePerFood;
    //            string s = score.ToString();
    //            scoreText.text = s;
    //        }
    //        else
    //        {
    //            score1 += scorePerFood;
    //            string sG = score.ToString();
    //            scoreText1.text = sG;
    //        }
    //    }
    //    else
    //    {
    //        if (photonView.IsMine)
    //        {
    //            score1 += scorePerFood;
    //            string sG = score.ToString();
    //            scoreText1.text = sG;
    //        }
    //        else
    //        {
    //            score += scorePerFood;
    //            string s = score.ToString();
    //            scoreText.text = s;
    //        }
    //    }
    //    gameObject.GetComponent<SnakeMovement>().moveSpeed += (scorePerFood / 2);
    //}

    //public void GrowSnakeS()
    //{
    //    score += scorePerFood;
    //    string s = score.ToString();

    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        if (photonView.IsMine)
    //        {
    //            scoreText.text = s;
    //        }
    //        else
    //        {
    //            scoreText1.text = s;
    //        }
    //    }
    //    else
    //    {
    //        if (photonView.IsMine)
    //        {
    //            scoreText1.text = s;
    //        }
    //        else
    //        {
    //            scoreText.text = s;
    //        }
    //    }
    //    gameObject.GetComponent<SnakeMovement>().moveSpeed += (scorePerFood / 2);
    //}



    //[PunRPC]
    //public void ShrinkSnakeScore()
    //{

    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        if (photonView.IsMine)
    //        {
    //            score -= scorePerFood;
    //            string s = score.ToString();
    //            scoreText.text = s;
    //        }
    //        else
    //        {
    //            score1 -= scorePerFood;
    //            string sS = score.ToString();
    //            scoreText1.text = sS;
    //        }
    //    }
    //    else
    //    {
    //        if (photonView.IsMine)
    //        {
    //            score1 -= scorePerFood;
    //            string sS = score.ToString();
    //            scoreText1.text = sS;
    //        }
    //        else
    //        {
    //            score -= scorePerFood;
    //            string s = score.ToString();
    //            scoreText.text = s;
    //        }
    //    }
    //    gameObject.GetComponent<SnakeMovement>().moveSpeed -= (scorePerFood / 5);
    //}

    //public void ShrinkSnakeS()
    //{
    //    score -= scorePerFood;
    //    string s = score.ToString();
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        if (photonView.IsMine)
    //        {
    //            scoreText.text = s;
    //        }
    //        else
    //        {
    //            scoreText1.text = s;
    //        }
    //    }
    //    else
    //    {
    //        if (photonView.IsMine)
    //        {
    //            scoreText1.text = s;
    //        }
    //        else
    //        {
    //            scoreText.text = s;
    //        }
    //    }
    //    gameObject.GetComponent<SnakeMovement>().moveSpeed -= (scorePerFood / 5);
    //}
    public void ShowScore()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if (photonView.IsMine)
            {
                totalScore = scoreText;
            }
            else
            {
                totalScore = scoreText1;
            }
        }
        else
        {
            if(photonView.IsMine)
            {
                totalScore = scoreText1;
            }
            else
            {
                totalScore = scoreText;
            }
        }
        
           
    }
}
