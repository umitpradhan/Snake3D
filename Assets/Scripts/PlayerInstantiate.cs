using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {      
        Instantiate(playerPrefab);        
    }
}

