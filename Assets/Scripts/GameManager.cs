using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject userNamePanel;
    public GameObject lobbyPanel;
    public GameObject createRoomPanel;
    public GameObject titlePanel;
    public GameObject modePanel;    
    public GameObject loadingPanel;
    public GameObject sPlayPanel;
    public GameObject playPanel;
    public GameObject gSignInPanel;

    public GameObject roomListPanel;
    

    private void Start()
    {
        StartCoroutine(LoadTitlePanel());
        
    }
    public void ActivatePanel(string panelName)
    {
        titlePanel.SetActive(panelName.Equals(titlePanel.name));
        lobbyPanel.SetActive(panelName.Equals(lobbyPanel.name));
        userNamePanel.SetActive(panelName.Equals(userNamePanel.name));
        createRoomPanel.SetActive(panelName.Equals(createRoomPanel.name));
        modePanel.SetActive(panelName.Equals(modePanel.name));        
        loadingPanel.SetActive(panelName.Equals(loadingPanel.name));
        sPlayPanel.SetActive(panelName.Equals(sPlayPanel.name));
        playPanel.SetActive(panelName.Equals(playPanel.name));
        gSignInPanel.SetActive(panelName.Equals(gSignInPanel.name));

        roomListPanel.SetActive(panelName.Equals(roomListPanel.name));
    }

    IEnumerator LoadTitlePanel()
    {
        ActivatePanel(titlePanel.name);
        
        yield return new WaitForSeconds(2.5f);

        ActivatePanel(modePanel.name);
    }


    public void SingleModePanel()
    {
        ActivatePanel(sPlayPanel.name);
    }
    public void MultiModePanel()
    {
        ActivatePanel(userNamePanel.name);
    }
}
