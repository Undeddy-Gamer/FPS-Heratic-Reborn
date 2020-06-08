﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScreen_old : MonoBehaviour
{
    // List of avalable quirks of type scriptable object
    [SerializeField] 
    private SoQuirk[] availableQuirks;    
    
    public GameObject quirkPanelPrefab;
    public GameObject quirksHolder;

    public PlayerSelection playerSelection;

    void Start()
    {
        SetupPanelSelection();
    }

    /// <summary>
    /// Add the Quirk panels from the sttached prefab to the quirk panel selection holder
    /// </summary>
    public void SetupPanelSelection()
    {
        GameObject tempQuirkPanel;
        foreach (SoQuirk quirk in availableQuirks)
        {
            tempQuirkPanel = Instantiate(quirkPanelPrefab);
            tempQuirkPanel.GetComponent<QuirkSelectPanel>().quirkSelect = quirk;
            tempQuirkPanel.transform.SetParent(quirksHolder.transform, false);
            tempQuirkPanel.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { playerSelection.SetQuirk(quirk); });
        }
    }

}
