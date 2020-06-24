using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField]
    int teamID;
    public Vector3 originalLocation;


    private void Start()
    {        
        originalLocation = transform.position;        
    }

    public void Update()
    {
       
        
    }

    //trigger for picking up the flag
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Flag Perimiter Entered");
        PickupFlag(other);
    }


    private void PickupFlag(Collider other)
    {
        //Player player = other.GetComponent<Player>();
        
        PlayerHandler_V2 player = other.GetComponent<PlayerHandler_V2>();

        if (player != null)
        {
            if (player.teamID == teamID)
            {
                return;
            }

            player.PickupWeapon(gameObject, originalLocation, teamID, 1);

            gameObject.SetActive(false);

        }
    }
}
