using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScreen : MonoBehaviour
{
    // List of avalable quirks of type scriptable object
    [SerializeField] 
    private SoQuirk[] availableQuirks;
    // List of avalable weapons of type scriptable object
    [SerializeField]
    public SoWeapon[] availableWeapons;

    public GameObject quirkPanelPrefab;
    public GameObject quirksHolder;

    public PlayerSelection playerSelection;

    void Start()
    {
        SetupPanelSelection();
    }
    
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
