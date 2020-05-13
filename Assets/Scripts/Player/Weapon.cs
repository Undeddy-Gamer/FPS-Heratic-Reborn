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

    public void SetWeaponGameObject(int teamID, GameObject worldGameObject, Vector3 originalLocation)
    {

        this.teamID = teamID;

        if(worldGameObject != null)
        {
            worldWeaponObject = worldGameObject;           
        }
        this.originalLocation = originalLocation;
    }


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
