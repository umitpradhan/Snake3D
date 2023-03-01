using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Linq;

public class Networkmanager : MonoBehaviourPunCallbacks
{
    public GameObject sHeadPrefab;
    public GameObject gameManager;
    //public GameObject backButton;
    public InputField userNameText;
    public InputField roomNameText;

    private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default); 
    private Dictionary<string, GameObject> roomListGameobject;
    private Dictionary<string, RoomInfo> roomListData;
    public GameObject roomListPrefab;
    public GameObject roomListParent;
    private GameObject roomlistItemObject;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        roomListData = new Dictionary<string, RoomInfo>();
        roomListGameobject = new Dictionary<string, GameObject>();
    }

    public void OnLoginClick()
    {
        string userName = userNameText.text;
        if (!string.IsNullOrEmpty(userName))
        {
            PhotonNetwork.LocalPlayer.NickName = userName;
            PhotonNetwork.ConnectUsingSettings(); 
            gameManager.GetComponent<GameManager>().ActivatePanel(gameManager.GetComponent<GameManager>().loadingPanel.name);
        }
    }

    public void OnClickCreateRoom()
    {
        string roomName = roomNameText.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            roomName = roomName + Random.Range(0, 10000);
            
        }
        CreateRoom(roomName);
        Debug.Log(roomName);
    }
    public void OnClickRoomListButton()
    {
        if(PhotonNetwork.InLobby)
        {
            Debug.Log("Is in lobby");
            PhotonNetwork.JoinLobby();
        }
        gameManager.GetComponent<GameManager>().ActivatePanel(gameManager.GetComponent<GameManager>().roomListPanel.name);
    }






    #region Photon_Callbacks


    public override void OnConnected()
    {
        Debug.Log("Connected to internet");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        JoinLobby();
        gameManager.GetComponent<GameManager>().ActivatePanel(gameManager.GetComponent<GameManager>().lobbyPanel.name);
    }  

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "Joined Room");
        PhotonNetwork.CurrentRoom.IsOpen= true;
        PhotonNetwork.CurrentRoom.IsVisible= true;

        if(PhotonNetwork.IsMasterClient)
        {
            gameManager.GetComponent<GameManager>().ActivatePanel(gameManager.GetComponent<GameManager>().playPanel.name);
        }               
    }

    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom + "is created");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("roomlist update");
        ClearList();
        foreach(RoomInfo rooms in roomList)
        {
            Debug.Log("Room Name" + rooms.Name);
            if(!rooms.IsOpen || !rooms.IsVisible || rooms.RemovedFromList)
            {
                if(roomListData.ContainsKey(rooms.Name))
                {
                    roomListData.Remove(rooms.Name);
                }
            }
            else
            {
                if (roomListData.ContainsKey(rooms.Name)) 
                {
                    roomListData[rooms.Name] = rooms;
                }
                else
                {
                    roomListData.Add(rooms.Name, rooms);
                }
            }
            
        }
        foreach(RoomInfo roomitem in roomListData.Values)
        {
            Debug.Log("roomlist create");
            roomlistItemObject = Instantiate(roomListPrefab);
            roomlistItemObject.transform.SetParent(roomListParent.transform);
            Debug.Log(roomListParent.transform.position + "content");
            Debug.Log(roomlistItemObject.transform.position + "roomlistItemObject");
            roomlistItemObject.transform.localScale = Vector3.one;
            roomlistItemObject.transform.GetChild(0).gameObject.GetComponent<Text>().text= roomitem.Name;
            roomlistItemObject.transform.GetChild(1).gameObject.GetComponent<Text>().text= roomitem.PlayerCount + "/" + roomitem.MaxPlayers;
            roomlistItemObject.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => RoomJoinFromlist(roomitem.Name));
            
            
            //backButton.gameObject.GetComponent<Button>().interactable= false;
            roomListGameobject.Add(roomitem.Name, roomlistItemObject);
        }
    }


    #endregion



    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby(customLobby);
    }
    public void ClearList()
    {
        if(roomListGameobject.Count > 0)
        {
            foreach (var v in roomListGameobject.Values)
            {
                Destroy(v);
            }
            roomListGameobject.Clear();
        }
        
    }

    public void RoomJoinFromlist(string roomName)
    {
        if(PhotonNetwork.InLobby)
        {
            //backButton.gameObject.GetComponent<Button>().interactable = false;
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.JoinRoom(roomName);
            //gameManager.GetComponent<GameManager>().ActivatePanel(gameManager.GetComponent<GameManager>().playPanel.name);
        }
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 2 }); 
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom(); 
    }

    public void OnClickCancel()
    {
        
        gameManager.GetComponent<GameManager>().ActivatePanel(gameManager.GetComponent<GameManager>().lobbyPanel.name);

    }
    public void OnClickBack()
    {
        //if(roomlistItemObject.transform.GetChild(2).gameObject.GetComponent<Button>()!= null)
        //{
        //    roomlistItemObject.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = false;
        //}        
        gameManager.GetComponent<GameManager>().ActivatePanel(gameManager.GetComponent<GameManager>().lobbyPanel.name);
        
    }

    public void LoadLevelP()
    {
        PhotonNetwork.LoadLevel(2);
        //PhotonNetwork.Instantiate("SnakeHead", Vector3.zero, Quaternion.identity); 
        
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
        //PhotonNetwork.Instantiate("SnakeHead", Vector3.zero, Quaternion.identity); 
        
    }
}
