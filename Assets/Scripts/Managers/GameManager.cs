using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance = null;
    //Make it so this script is accesable anywhere in the scene 

    /*
    public GameObject highScorePanel;
    public GameObject highScoreList = null;
    public GameObject txtPrefab;
    */

    void Start()
    {
        Instance = this;
        //Load and display HighScores
        
    }
    #endregion
    //Restarts current Level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Loads next level
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    //Loads Previous Level
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
