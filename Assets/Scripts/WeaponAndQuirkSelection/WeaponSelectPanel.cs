using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  Selection panel for selecting a weapon. Multiple instances are added to the selection screen based on SoWeapon scriptable objects
/// </summary>
public class WeaponSelectPanel : MonoBehaviour
{
    public SoWeapon weaponSelect;
    public Text nameText;
    public Text descriptionText;
    public Image iconImage;

    // Start is called before the first frame update
    void Start()
    {
        SetupPanel();
    }

    public void SetupPanel()
    {
        if (weaponSelect)
        {
            nameText.text = weaponSelect.weaponName;
            descriptionText.text = weaponSelect.weaponDescription;
            iconImage.sprite = weaponSelect.weaponIcon;
        }
    }
    public void SetSelected()
    {
        WeaponSelectPanel[] tempPanels = transform.parent.GetComponentsInChildren<WeaponSelectPanel>();
        
        foreach (WeaponSelectPanel panel in tempPanels)
        {
            panel.GetComponent<Image>().enabled = false;
        }

        this.GetComponent<Image>().enabled = true;
    }
}
