using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PUNInstantiate : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        
        if(PhotonNetwork.IsConnectedAndReady)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(((Screen.width / 2) / 2) * (-1), 24, 0), Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(((Screen.width / 2) / 2), 24, 0), Quaternion.identity);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
