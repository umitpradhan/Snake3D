using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HostGame : MonoBehaviourPunCallbacks
{
    public void Host()
    {
        PhotonNetwork.CreateRoom("MyGame", new RoomOptions { MaxPlayers = 2 });
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room: " + message);
    }
}
