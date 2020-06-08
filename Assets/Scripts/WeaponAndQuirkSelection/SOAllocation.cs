using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOAllocation : MonoBehaviour
{
    public SoQuirk myQuirk;
    public SoWeapon mySOWeapon;
    public Player player;

    [SerializeField] private GameObject weaponHolder;

    void Start()
    {
        
        if (mySOWeapon.weaponPrefab != null)
        { 
            // add weapon to player
            AddWeaponToPlayer(mySOWeapon.weaponPrefab);
        }
    }

    private void AddWeaponToPlayer(GameObject weaponPrefab)
    {

    }

}
