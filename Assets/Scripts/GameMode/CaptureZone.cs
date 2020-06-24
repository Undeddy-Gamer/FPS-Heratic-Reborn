using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Capture zone class for capture the flag mode
/// </summary>
public class CaptureZone : MonoBehaviour
{
    [SerializeField]
    int teamID;
    GameModeCTF gameModeCTF;
    
    private void Start()
    {
        gameModeCTF = FindObjectOfType<GameModeCTF>();

        if(gameModeCTF == null)
        {
            Debug.Log("No Capture Area");
        }
    }


    /// <summary>
    /// trigger capture flag when player enters the capture zone perimiter
    /// </summary>
    /// <param name="other">the collider that enters</param>
    private void OnTriggerEnter(Collider other)
    {
        //Player player = other.GetComponent<Player>();
        PlayerHandler_V2 player = other.GetComponent<PlayerHandler_V2>();

        if (player != null)
        {
            if (player.GetWeaponTeamID() == teamID)
            {
                return;
            }

            if (player.isHoldingFlag())
            {
                gameModeCTF.AddScore(player.teamID, 1);
                player.ReturnWeapon(1);
            }
        }
    }
}
