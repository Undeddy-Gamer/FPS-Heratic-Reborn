using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allocats the weapon and other options chosen before joining the game to the player
/// </summary>
public class SOAllocation : MonoBehaviour
{
    public SoQuirk myQuirk;
    public SoWeapon mySOWeapon;
    public PlayerHandler_V2 player;
    public GameObject weaponHolder;
    public string selectedWeapon;
    public GameObject backupWeapon;

    // List of avalable weapons of type scriptable object
    [SerializeField] private SoWeapon[] availableWeapons;

    void Start()
    {        
        foreach (SoWeapon wep in availableWeapons)
        {
            if(wep.weaponName == selectedWeapon)
            {
                mySOWeapon = wep;
                AddWeaponToPlayer(mySOWeapon.weaponPrefab);
            }
        }

        //if lobby selected weapon wasn't selected for some reason activate the default backup weapon
        if (mySOWeapon == null && backupWeapon != null)
        {
            backupWeapon.SetActive(true);
            player.GetComponent<PlayerHandler_V2>().weapons[0] = backupWeapon.GetComponent<Weapon>();
        }

    }

    /// <summary>
    /// Add the main weapon to the player, checks if it is a staff built for testing in multiplayer
    /// </summary>
    /// <param name="weaponPrefab">the prefab of the weapon to add to the player</param>
    private void AddWeaponToPlayer(GameObject weaponPrefab)
    {

        GameObject weaponInstance = Instantiate(weaponPrefab, weaponHolder.transform.position, weaponHolder.transform.rotation, weaponHolder.transform);

        if (player.GetComponent<MovementController_MP>() != null)
        {
            if (weaponInstance.GetComponent<Weapon>().asociatedMPStaff != null)
            {
                weaponInstance.GetComponent<Weapon>().asociatedMPStaff.cam = player.GetComponent<MovementController_MP>().playerCamera.GetComponent<Camera>();
                weaponInstance.GetComponent<Weapon>().asociatedMPStaff.player = player.GetComponent<PlayerHandler_V2>();                
            }
            else
            {
                weaponInstance.GetComponent<Weapon>().asociatedStaff.cam = player.GetComponent<MovementController_MP>().playerCamera.GetComponent<Camera>();
                weaponInstance.GetComponent<Weapon>().asociatedStaff.player = player.GetComponent<PlayerHandler_V2>();                
            }
            player.GetComponent<PlayerHandler_V2>().weapons[0] = weaponInstance.GetComponent<Weapon>();


            //player.GetComponent<MovementController_MP>()
        }

    }

}
