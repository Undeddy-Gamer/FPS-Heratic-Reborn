
using UnityEngine;

[CreateAssetMenu]
public class SoWeapon : ScriptableObject
{
    public string weaponName;
    public string weaponDescription;

    public Sprite weaponIcon;
    public GameObject weaponPrefab;

    public float weaponDamage;
    public float weaponRange;
    public float weaponFireRate;
    public int weaponMaxUnchangedAmmo;
    public float weaponReloadTime;
}
