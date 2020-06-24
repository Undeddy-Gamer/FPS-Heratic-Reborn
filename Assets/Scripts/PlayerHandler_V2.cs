using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
public class PlayerHandler_V2 : MonoBehaviour
{
    [Header("Value Variables")]
    public float curHealth = 100;
    public float curMana = 30, curStamina = 100;
    public float maxHealth = 100, maxMana =  30, maxStamina = 100, healRate = 0.1f;
    public float storedMana = 500;
    
    private float prevHealth, prevMana, prevStamina;
    private float healTimer;

    Rigidbody playerRigidbody;
    //[SerializeField]
    //public Stats[] stats;

    [Header("HUD Variables")]
    public Slider healthBar;
    public Slider manaBar, staminaBar;
    public Text healthText;
    public Text manaText;
    public Text storedManaText;
    public Image weaponIcon;


    [Header("Damage Effect Variables")] 
    static public bool isDead;
    bool damaged;
    bool canHeal = false;

    [Header("Check Point")]
    public Transform curCheckPoint;

    [Header("Weapon Stuff")]
    public List<Weapon> weapons;    
    public int currentWeapon = 0;
    public int lastWeapon = 0;
    public float forwardDropOffset = 4;
    public float upDropOffset = 1;

    [Header("Team Stuff")]
    [SerializeField]
    int playersTeamID = 0;
    public int teamID { get { return playersTeamID; } }


    // Start is called before the first frame update
    void Start()
    {
        SwitchWeapon(currentWeapon, true);

        playerRigidbody = this.GetComponent<Rigidbody>();
        if (playerRigidbody == null)
        {
            Debug.Log("No Player Rigidbody");
        }
                
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.X))
        {
            DropWeapon(currentWeapon);
        }


        //Player is Dead
        if (curHealth <= 0 && !isDead)
        {
            Death();
        }



        #if UNITY_EDITOR
        //function to test damage
        //Test Damage
        if (Input.GetKeyDown(KeyCode.X))
                {
                    curHealth -= 5;
                    damaged = true;
                }
        #endif

       
        if (!canHeal && curHealth < maxHealth && curHealth > 0)
        {
            healTimer += Time.deltaTime;
            if(healTimer >= 7)      
            {
                canHeal = true;
            }
        }

        // set health bar (to-do: optimise)
        if (healthBar != null)
        {
            if (healthBar.value != curHealth / maxHealth)
            {
                curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
                healthBar.value = Mathf.Clamp01(curHealth / maxHealth);
            }
        }

        //set mana bar  (to-do: optimise)
        if (manaBar != null)
        {
            if (manaBar.value != curMana / maxMana)
            {
                curMana = Mathf.Clamp(curMana, 0, maxMana);
                manaBar.value = Mathf.Clamp01(curMana / maxMana);
            }
        }


        // Set the health and mana text (to-do: optimise)
        if (healthText != null && manaText != null)
        {
            healthText.text = System.Math.Round(curHealth) + " / " + maxHealth;
            manaText.text = System.Math.Round(curMana) + " / " + maxMana;
            storedManaText.text = "Stored Mana: " + System.Math.Round(storedMana);
        }

        //Change weapon
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            changeCurrentWeapon(1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            changeCurrentWeapon(-1);
        }
    }

    private void LateUpdate()
    {
        if (curHealth < maxHealth && curHealth > 0 && canHeal)
        {
            HealOverTime();
        }
    }


    /// <summary>
    /// set the death indicator to true
    /// </summary>
    void Death()
    {
        // set the death flag to this funciton int's called again
        isDead = true;                
    }


   /// <summary>
   /// Revive Player function
   /// </summary>
    void Revive()
    {
        isDead = false;
        curHealth = maxHealth;
        curMana = maxMana;
        curStamina = maxStamina;

        //move and rotate to spawn location

        //spawnpoint spawnPoints.ElementAtOrDefault(nextIndex);

        this.transform.position = curCheckPoint.position;
        this.transform.rotation = curCheckPoint.rotation;

    }

    
    /// <summary>
    /// Damage player function
    /// </summary>
    /// <param name="damage"></param>
    public void DamagePlayer(float damage)
    {
        damaged = true;
        canHeal = false;
        curHealth -= damage;
        healTimer = 0;
        //Invoke("Healable", 10f);
    }

    /// <summary>
    /// Function to heal over time if player has heal rate de
    /// </summary>
    public void HealOverTime()
    {
        //curHealth += Time.deltaTime * (healRate + stats[2].statValue);
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

    public void changeCurrentWeapon(int direction, bool overrideLock = false)
    {
        if (weapons[currentWeapon].isWeaponLocked && !overrideLock)
        {
            return;
        }
        if (weapons[currentWeapon + direction] != null)
        { 
            if (weapons[currentWeapon + direction].isWeaponLocked)
            {
                direction *= 2;
            }
        }

        int weaponToSwitch = currentWeapon;
        if (weaponToSwitch + direction < 0)
        {
            weaponToSwitch = weapons.Count -1;
        }
        else if (weaponToSwitch + direction > weapons.Count - 1)
            weaponToSwitch = 0;
        else
            weaponToSwitch += direction;

        SwitchWeapon(weaponToSwitch, overrideLock);
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
        weaponIcon.sprite = weapons[currentWeapon].weaponIcon;
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

        if (weapons[weaponID].isWeaponDropable)
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

            SwitchWeapon(lastWeapon, true); //if possible
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
