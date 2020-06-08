using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]    
    [SerializeField] private GameObject landingPagePanel;
    [SerializeField] private InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;


    public void Start()
    {
        if(networkManager == null)
        {
            Debug.LogError("newtworkManager not attached to JoinLobbyMenu");
        }

        if (landingPagePanel == null)
        {
            Debug.LogError("landingPagePanel not attached to JoinLobbyMenu");
        }

        if (ipAddressInputField == null)
        {
            Debug.LogError("ipAddressInputField not attached to JoinLobbyMenu");
        }

        if (joinButton == null)
        {
            Debug.LogError("joinButton not attached to JoinLobbyMenu");
        }

    }

    private void OnEnable()
    {
        networkManager.onClientConnected += HandleClientConnected;
        networkManager.onClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        
    }

    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;
        
        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();

        joinButton.interactable = false;
    }


    private void HandleClientConnected()
    {
        joinButton.interactable = true;

        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }
}
