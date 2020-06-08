using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class Player : NetworkBehaviour
{

    

    // GameModeCTF gameModeCTF;

    // fix all of this up and add to     

    public List<Weapon> weapons;

    Rigidbody playerRigidbody;
    [SerializeField]
    int playersTeamID = 0; 
    public int teamID { get { return playersTeamID; } }

    int currentWeapon = 0;
    int lastWeapon = 0;

    public float forwardDropOffset = 4;
    public float upDropOffset = 1;



    private void Start()
    {
        SwitchWeapon(currentWeapon, true);

        playerRigidbody = this.GetComponent<Rigidbody>();
        if(playerRigidbody == null)
        {
            Debug.Log("No Player Rigidbody");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DropWeapon(currentWeapon);
        }
        
    }


    /// <summary>
    /// Pickup weapon and switch to it
    /// </summary>
    /// <param name="weaponObject">the weapon gameobject to set as active</param>
    /// <param name="originalLocation">the origianl location of the weapon (oftern the place where it was picked up)</param>
    /// <param name="teamID">if the weapon has a team only association set it here</param>
    /// <param name="weaponID">the ID in the weapon list of the player to change to and enable</param>
    /// <param name="overrideLock">overide lock is used when a weapon object (eg flag) cannont be changed from</param>
    /// <example>
    /// <code>
    ///     PickupWeapon(weaponObject, OriginalLocation, teamID, weaponID, true);
    /// </code>
    /// </example>    
    public void PickupWeapon(GameObject weaponObject, Vector3 originalLocation, int teamID, int weaponID, bool overrideLock = false)
    {
        //add weapon object to inventory here
        SwitchWeapon(weaponID, overrideLock);
        weapons[weaponID].SetWeaponGameObject(teamID, weaponObject, originalLocation);
    }

    /// <summary>
    /// Switch to weapon if able to (some weapon items like the flag cannot be changed from)
    /// </summary>
    /// <param name="weaponID"></param>
    /// <param name="overrideLock">overide lock is used when a weapon object (eg flag) cannont be changed from, use this to force an overide</param>
    /// <example>
    /// <code>
    ///     PickupWeapon(weaponObject, OriginalLocation, teamID, weaponID, true);
    /// </code>
    /// </example>  
    public void SwitchWeapon(int weaponID, bool overrideLock = false)
    {
        if (weapons[currentWeapon].isWeaponLocked && !overrideLock)
        {
            return;
        }

        lastWeapon = currentWeapon;
        currentWeapon = weaponID;
        
        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        weapons[currentWeapon].gameObject.SetActive(true);
    }

    /// <summary>
    /// Drop weapon by weapon index (ID)
    /// </summary>
    /// <param name="weaponID">the index of the weapon in the player weapon list</param>
    /// <example>
    /// <code>
    ///     DropWeapon(0);
    /// </code>
    /// </example>    
    public void DropWeapon(int weaponID)
    {
        // fix all of this up
        
        if(weapons[weaponID].isWeaponDropable)
        {
            // get drop offset
            Vector3 forward = transform.forward;
            forward.y = 0;
            forward *= forwardDropOffset;
            forward.y = upDropOffset;

            Vector3 dropLocation = transform.position + forward;

            weapons[weaponID].DropWeapon(playerRigidbody, dropLocation);
            weapons[weaponID].worldWeaponObject.SetActive(true);            

            SwitchWeapon(lastWeapon, true); //if possible
        }
    }


    /// <summary>
    /// Return weapon world object to it's original position (currently only works for flag weapon object)
    /// </summary>
    /// <param name="weaponID">the ID of the weapon to return</param>
    /// <example>
    /// <code>
    ///     ReturnWeapon(0);
    /// </code>
    /// </example>
    public void ReturnWeapon(int weaponID)
    {
        if (weaponID == 1) // flag
        {            
            Vector3 returnLocation = weapons[weaponID].originalLocation;           

            weapons[weaponID].worldWeaponObject.transform.position = returnLocation;
            weapons[weaponID].worldWeaponObject.SetActive(true);

            SwitchWeapon(lastWeapon); //if possible
        }
    }

    /// <summary>
    /// check if player is holding the flag
    /// </summary>
    public bool isHoldingFlag()
    {
        if (currentWeapon == 1)
            return true;
        else
            return false;

    }


    public int GetWeaponTeamID()
    {
        return weapons[currentWeapon].teamID;
    }
}
