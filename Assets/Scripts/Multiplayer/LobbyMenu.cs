using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField]
    private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;


    public void Start()
    {
        if (networkManager == null)
        {
            Debug.LogError("networkManager not attached to main menu");
        }

        if (landingPagePanel == null)
        {
            Debug.LogError("landingPagePanel not attached to main menu");
        }
    }

    public void HostLobby()
    {
        networkManager.StartHost();

        landingPagePanel.SetActive(false);
    }

}
