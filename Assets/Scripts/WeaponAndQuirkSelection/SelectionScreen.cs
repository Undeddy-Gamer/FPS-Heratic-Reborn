using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Selection screen class where you select your weapon before you enter the lobby, adds QuirkSelectPanel and WeaponSelectPanel based on available SoQuirk and SoWeapon objects set.
/// </summary>
public class SelectionScreen : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private InputField nameInputField = null;
    [SerializeField] private Button continueButton = null;
    public GameObject quirkPanelPrefab;
    public GameObject quirksHolder;
    public GameObject weaponPanelPrefab;
    public GameObject weaponsHolder;

    // List of avalable quirks of type scriptable object
    [SerializeField] private SoQuirk[] availableQuirks;
    // List of avalable weapons of type scriptable object
    [SerializeField] private SoWeapon[] availableWeapons;

    //Player Selections
    public static SoWeapon SelectedWeapon { get; private set; }
    public static SoQuirk SelectedQuirk { get; private set; }
    public static string DisplayName { get; private set; }

    private string PlayerPrefsNameKey = "PlayerName";
    private string PlayerPrefsQuirkKey = "PlayerQuirk";
    private string PlayerPrefsWeaponKey = "PlayerWeapon";

    /// <summary>
    /// Is true when all player selections are made
    /// </summary>
    private bool PlayerSelectionsMade
    {
        get
        {
            if (PlayerPrefs.HasKey(PlayerPrefsNameKey) && PlayerPrefs.HasKey(PlayerPrefsQuirkKey) && PlayerPrefs.HasKey(PlayerPrefsWeaponKey))
                return true;
            else
                return false;
        }
    }


    private void Start() // => SetUpInputField()
    {
        if (nameInputField == null)
        {
            Debug.LogError("nameInputField not attached to PlayerInput");
        }

        if (continueButton == null)
        {
            Debug.LogError("continueButton not attached to PlayerInput");
        }
        // setup the selection panels
        SetupPanelSelection();        
    }

    /// <summary>
    /// This creates the panels to select the weapons etc from the prefabs sets up the panels and adds them to the selection areas. 
    /// </summary>
    public void SetupPanelSelection()
    {
        GameObject tempQuirkPanel;
        foreach (SoQuirk quirk in availableQuirks)
        {
            tempQuirkPanel = Instantiate(quirkPanelPrefab);
            tempQuirkPanel.GetComponent<QuirkSelectPanel>().quirkSelect = quirk;
            tempQuirkPanel.transform.SetParent(quirksHolder.transform, false);
            tempQuirkPanel.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { SetQuirk(quirk); });

            if (PlayerPrefs.HasKey(PlayerPrefsQuirkKey))
            {
                if (PlayerPrefs.GetString(PlayerPrefsQuirkKey) == quirk.quirkName)
                {
                    tempQuirkPanel.GetComponent<QuirkSelectPanel>().SetSelected();
                    SelectedQuirk = quirk;
                }
            }
        }


        GameObject tempWeaponPanel;
        foreach (SoWeapon weapon in availableWeapons)
        {
            tempWeaponPanel = Instantiate(weaponPanelPrefab);
            tempWeaponPanel.GetComponent<WeaponSelectPanel>().weaponSelect = weapon;
            tempWeaponPanel.transform.SetParent(weaponsHolder.transform, false);
            tempWeaponPanel.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { SetWeapon(weapon); });

            if (PlayerPrefs.HasKey(PlayerPrefsWeaponKey))
            {
                if (PlayerPrefs.GetString(PlayerPrefsWeaponKey) == weapon.weaponName)
                {
                    tempWeaponPanel.GetComponent<WeaponSelectPanel>().SetSelected();
                    SelectedWeapon = weapon;
                }
            }
        }

        if (PlayerPrefs.HasKey(PlayerPrefsNameKey))
        {
            nameInputField.text = PlayerPrefs.GetString(PlayerPrefsNameKey);
            DisplayName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        }

        continueButton.interactable = PlayerSelectionsMade;

        nameInputField.onValueChanged.AddListener(delegate { SavePlayerName(); });
    }

    /// <summary>
    /// Set the display name for the player
    /// </summary>
    public void SavePlayerName()
    {
        if (!string.IsNullOrEmpty(nameInputField.text))
        { 
            DisplayName = nameInputField.text;
            PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
        }
        continueButton.interactable = PlayerSelectionsMade;
    }
    /// <summary>
    /// Set the selected quirk for the player
    /// </summary>
    /// <param name="selectedQuirk"></param>
    public void SetQuirk(SoQuirk selectedQuirk)
    {
        SelectedQuirk = selectedQuirk;
        PlayerPrefs.SetString(PlayerPrefsQuirkKey, selectedQuirk.quirkName);
        continueButton.interactable = PlayerSelectionsMade;
    }
    /// <summary>
    /// set the selected weapon for the player
    /// </summary>
    /// <param name="selectedWeapon"></param>
    public void SetWeapon(SoWeapon selectedWeapon)
    {
        SelectedWeapon = selectedWeapon;
        PlayerPrefs.SetString(PlayerPrefsWeaponKey, selectedWeapon.weaponName);
        continueButton.interactable = PlayerSelectionsMade;
    }

    

}
