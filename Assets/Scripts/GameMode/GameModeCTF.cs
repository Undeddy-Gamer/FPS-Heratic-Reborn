using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Modified game mode for capture the flag GameMode
/// </summary>
public class GameModeCTF : GameMode
{
    [SerializeField]
    GameObject flagCapturePanel;
    [SerializeField]
    TMP_Text flagCounter;
    List<Flag> flags;


    private void Start()
    {
        if (flagCapturePanel != null)
        {
            flagCapturePanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (flagCapturePanel != null)
        {
            if (flagCounter != null)
            {
                flagCounter.text = "<color=red>" + teams[0].score + "</color>      <color=blue>" + teams[1].score + "</color>";
            }
        }
    }
}
