using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuirkSelectPanel : MonoBehaviour
{
    public SoQuirk quirkSelect;
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
        if (quirkSelect)
        {
            nameText.text = quirkSelect.quirkName;
            descriptionText.text = quirkSelect.description;
            iconImage.sprite = quirkSelect.Icon;
        }
    }
    
}
