using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public GameObject endGamePanel;


    public void ActivatePanel(string panelName)
    {
        endGamePanel.SetActive(panelName.Equals(endGamePanel.name));

    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(0);
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
}
