using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
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

    public void PickupWeapon(GameObject weaponObject, Vector3 originalLocation, int teamID, int weaponID, bool overrideLock = false)
    {

        SwitchWeapon(weaponID, overrideLock);

        weapons[weaponID].SetWeaponGameObject(teamID, weaponObject, originalLocation);
    }

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
