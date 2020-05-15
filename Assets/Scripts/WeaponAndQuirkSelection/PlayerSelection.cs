using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    
    public SoQuirk selectedQuirk;
    public SoWeapon selectedWeapon;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //private void Start()
    //{
    //    Debug.Log(selectedQuirk.quirkName);
    //}

    public void SetQuirk(SoQuirk selectQuirk)
    {
        selectedQuirk = selectQuirk;
        Debug.Log(selectedQuirk.quirkName);
    }


}
