using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public bool inGameMenuOpen = false;

    //Make it so this script is accesable anywhere in the scene 
    #region Singleton
    public static GameManager Instance = null;   
    
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
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    
    //Loads Previous Level
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
