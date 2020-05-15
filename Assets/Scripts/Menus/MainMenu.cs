using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject pnlSettings;
    public GameObject pnlMainMenu;
    public GameObject pnlWeaponQuirkSelect;
    

    public int sceneCTF = 2;
    public int sceneDM = 1;


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
            pnlMainMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked; // lock the mouse cursor
            Cursor.visible = false;
            GameManager.Instance.inGameMenuOpen = false;
            return;
        }
        else
        {           
            Cursor.lockState = CursorLockMode.None; // lock the mouse cursor
            Cursor.visible = true;
            pnlMainMenu.SetActive(true);
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
        if (pnlMainMenu != null)
        { 
            pnlMainMenu.SetActive(false);
        }
        if (pnlWeaponQuirkSelect != null)
        {
            pnlWeaponQuirkSelect.SetActive(false);
        }
    }

    public void ShowMainMenu()
    {
        if (pnlSettings != null)
        {
            pnlSettings.SetActive(false);
        }
        if (pnlMainMenu != null)
        {
            pnlMainMenu.SetActive(true);
        }
        if (pnlWeaponQuirkSelect != null)
        {
            pnlWeaponQuirkSelect.SetActive(false);
        }
    }

    public void ShowWeaponQuirkMenu()
    {
        if (pnlSettings != null)
        {
            pnlSettings.SetActive(false);
        }
        if (pnlMainMenu != null)
        {
            pnlMainMenu.SetActive(false);
        }
        if (pnlWeaponQuirkSelect != null)
        {
            pnlWeaponQuirkSelect.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.LoadScene(0);
    }

    public void PlayCaptureTheFlag()
    {
        GameManager.Instance.LoadScene(sceneCTF);
    }

    public void PlayDeathMatch()
    {
        GameManager.Instance.LoadScene(sceneDM);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif 
        Application.Quit();
    }

}
