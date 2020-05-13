using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSave : MonoBehaviour
{
    public PlayerHandler player;
    private void Start()
    {
        if(!PlayerPrefs.HasKey("Loaded"))
        {
            PlayerPrefs.DeleteAll();
            //FirstLoad();
            PlayerPrefs.SetInt("Loaded", 0);
            Save();
        }
        else
        {
            //Load Binary Stuff
            Load();
        }
    }

    public void Save()
    {

        
        //Saves Player Stats
        //Health, Mana, Stamina
        PlayerPrefs.SetFloat("CurHealth", player.curHealth);
        PlayerPrefs.SetFloat("CurStamina", player.curStamina);
        PlayerPrefs.SetFloat("CurMana", player.curMana);
        
        //Position
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);

        //Rotation        
        PlayerPrefs.SetFloat("RotationX", player.transform.rotation.x);
        PlayerPrefs.SetFloat("RotationY", player.transform.rotation.y);
        PlayerPrefs.SetFloat("RotationZ", player.transform.rotation.z);
        PlayerPrefs.SetFloat("RotationW", player.transform.rotation.w);
        
    }


    public void Load()
    {
       
        //Players current health is set to playerPrefs saved float called CurHealth, else set it to MaxHealth
        player.curHealth = PlayerPrefs.GetFloat("CurHealth", player.maxHealth);
        player.curStamina = PlayerPrefs.GetFloat("CurStamina", player.maxStamina);
        player.curMana = PlayerPrefs.GetFloat("CurMana", player.maxMana);

        //Get the player poisition x.y,z and put it into a vector3 and set the new player position
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX", 1), PlayerPrefs.GetFloat("PlayerY", 1), PlayerPrefs.GetFloat("PlayerZ", 1));
        
        //Get the saved rotation of the character and set via a Quaternion (4 floats, X,Y,Z,W)
        player.transform.rotation = new Quaternion(PlayerPrefs.GetFloat("RotationX", 0), PlayerPrefs.GetFloat("RotationY", 0), PlayerPrefs.GetFloat("RotationZ", 0), PlayerPrefs.GetFloat("RotationW", 0));
       

    }

    /*
    public void FirstLoad()
    {
        
        PlayerDataToSave data = PlayerSaveToBinary.LoadData(player);

        //player.name = data.playerName;

        player.curCheckPoint = GameObject.Find("Home Checkpoint 1").GetComponent<Transform>();

        player.maxHealth = data.maxHealth;
        player.maxMana = data.maxMana;
        player.maxStamina = data.maxStamina;

        player.curHealth = data.curHealth;
        player.curMana = data.curMana;
        player.curStamina = data.curStamina;

        player.transform.position = new Vector3(data.pX, data.pY, data.pZ);
        player.transform.rotation = new Quaternion(data.rX, data.rY, data.rZ, data.rW);        

    }
    */
}
