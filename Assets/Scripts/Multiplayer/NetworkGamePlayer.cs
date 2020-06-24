using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


/// <summary>
/// The network game player used to spawn in the player objects into the level
/// </summary>
public class NetworkGamePlayer : NetworkBehaviour
{

    [SyncVar]
    public string displayName = "Loading...";
    public string selectedWeaponStr;
    public float skillLevel;

    private NetworkManagerLobby room;

    private NetworkManagerLobby Room
    { 
        get 
        {
            if(room != null)
            {
                return room;
            }
            room = NetworkManager.singleton as NetworkManagerLobby;
            return room;
        } 
    }

   
    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);

        Room.GamePlayers.Add(this);
    }

    public override void OnNetworkDestroy()
    {
        Room.GamePlayers.Remove(this);
    }
    
    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    [Server]
    public void SetWeapnStr(string weaponName)
    {
        this.selectedWeaponStr = weaponName;
    }

}
