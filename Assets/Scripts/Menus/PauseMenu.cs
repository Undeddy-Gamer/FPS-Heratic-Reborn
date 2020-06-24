using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pnlSettings;
    public GameObject pnlPauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        if (GameManager.Instance.inGameMenuOpen)
        {
            pnlPauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked; // lock the mouse cursor
            Cursor.visible = false;
            GameManager.Instance.inGameMenuOpen = false;
            return;
        }
        else
        {           
            Cursor.lockState = CursorLockMode.None; // lock the mouse cursor
            Cursor.visible = true;
            pnlPauseMenu.SetActive(true);
            GameManager.Instance.inGameMenuOpen = true;
            return;
        }
    }

    public void ShowSettings()
    {
        if (pnlSettings != null)
        {
            pnlSettings.SetActive(true);
        }
        if (pnlPauseMenu != null)
        {
            pnlPauseMenu.SetActive(false);
        }
       
    }

    public void ShowMainMenu()
    {
        if (pnlSettings != null)
        {
            pnlSettings.SetActive(false);
        }
        if (pnlPauseMenu != null)
        {
            pnlPauseMenu.SetActive(true);
        }        
    }


    public void ReturnToMainMenu()
    {
        GameManager.Instance.LoadScene(0);
    }

    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif 
        Application.Quit();
    }

}
