using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLobbyInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TMP_InputField nameInputField = null;
    [SerializeField]
    private Button continueButton = null;

    public static string DisplayName { get; private set; }

    private string PlayerPrefsNameKey = "PlayerName";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
