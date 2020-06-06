using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int teamID = 0;

    public bool isWeaponLocked = false;
    public bool isWeaponDropable = false;

    public GameObject worldWeaponObject;
    public Vector3 originalLocation;

    /// <summary>
    /// Setup Weapon 'in game' object, also set it original position if it requires to be be respawned
    /// </summary>
    /// <param name="teamID">the id of the team the weapon belowngs to in case of flag or objective object</param>
    /// <param name="worldGameObject">the game object that will be shown in the world</param>
    /// <param name="originalLocation">set the original position of the weapon game object for it to return/respawn to once used or lost</param>
    /// <example>
    /// <code>
    ///     SetWeaponGameObject(0, flagWorldObject, flagWorldObject.originalPosition);
    /// </code>
    /// </example>    
    public void SetWeaponGameObject(int teamID, GameObject worldGameObject, Vector3 originalLocation)
    {
        this.teamID = teamID;

        if(worldGameObject != null)
        {
            worldWeaponObject = worldGameObject;           
        }
        this.originalLocation = originalLocation;
    }

    /// <summary>
    /// Drops the currently equipped weapon at the position specified in drop location (if able to do so), also adds player movement momentem to the item when dropped
    /// </summary>
    /// <param name="player">the rigid body of the player object, used to apply extra momentem on the wepon object to be dropped</param>
    /// <param name="droplocation">the position as a vector3 to drop the weapon object</param>
    /// <example>    
    /// <code>
    ///     DropWeapon(playerRigidBody, );
    /// </code>
    /// </example>
    public void DropWeapon(Rigidbody player, Vector3 droplocation)
    {
        float distanceToDrop = Vector3.Distance(Camera.main.transform.position, droplocation);
        Vector3 directionToDrop = (droplocation - Camera.main.transform.position).normalized;


        //ray to drop location
        Ray rayToDropLocation = new Ray(Camera.main.transform.position, directionToDrop);
        RaycastHit raycastHit;
        
        if (Physics.Raycast(rayToDropLocation, out raycastHit, distanceToDrop))
        {
            Debug.Log("raycast hit");
            droplocation = raycastHit.point;
        }


        //set the position in the world
        worldWeaponObject.transform.position = droplocation;

        
        // ray cast down (ground check)
        Renderer rend = worldWeaponObject.GetComponent<Renderer>();        
        if (rend != null)
        {
            Vector3 topPoint = rend.bounds.center;
            topPoint.y += rend.bounds.extents.y;

            float height = rend.bounds.extents.y * 2;

            //Debug.Log(height);
            //Debug.DrawRay(topPoint, Vector3.down * height * 1.1f, Color.red);

            Ray rayDown = new Ray(topPoint, Vector3.down);
            RaycastHit raycastHitDown = new RaycastHit();
            if (Physics.Raycast(rayDown, out raycastHitDown, height * 1.1f))
            {
                droplocation = raycastHit.point;
                droplocation.y += rend.bounds.extents.y + .5f;
            }

        }

        //set the position in the world
        worldWeaponObject.transform.position = droplocation;
        

        Rigidbody flagRigid = worldWeaponObject.GetComponent<Rigidbody>();

        if (flagRigid != null && player != null)
        {
            flagRigid.velocity = player.velocity * 2;
        }
    }
}
