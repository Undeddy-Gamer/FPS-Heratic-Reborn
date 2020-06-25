using System.Collections;
using UnityEngine;

/// <summary>
/// the Staff script which controls the specific  (not working)
/// </summary>
public class Staff : MonoBehaviour
{

    //Most of this stuff should be moved to the weapon script excluding the reload functionality for the animation trigger

    public float damage = 10f;
    public float range = 100f;
    public float manaShotCost = 5f;
    public float projectileSpeed = 5;

    // the Higher the faster the rate of fire
    public float rateOfFire = 10f;

    //the camera of the player to use for raycast hit detection
    public Camera cam;

    
    // a prefab for where the weapon hits
    public GameObject impactFX;
    // weapon animator
    public Animator weaponAnimator;
    // the player that will be using the Staff
    public PlayerHandler_V2 player;

    [SerializeField]
    private ParticleSystem weaponFire;
    [SerializeField]
    private GameObject weaponFirePrefab;
    [SerializeField]
    private ParticleSystem staffRechargeParticles;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private GameObject shootPosition;
    

    private float canFire = 0f;
    

    private void Update()
    {



        if (Input.GetButton("Fire1") && Time.time >= canFire)
        {
            canFire = Time.time + 1f / rateOfFire;
            Shoot();
        }

        //Reload Weapon
        if (Input.GetKeyDown("r"))
        {
            if (player.storedMana > 0)
            { 
                //play reload animation, it has a trigger for when to apply the reload
                weaponAnimator.Play("StaffReload",-1,0f);
            }
        }

        //Change weapon
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            //trigger is attached to activate changeCurrentWeapon()
            weaponAnimator.Play("ChangeEquip", -1, 0f);
        }
        
    }

    /// <summary>
    /// Change weapon function for animation event
    /// </summary>
    public void changeCurrentWeapon()
    {
        int weaponDirectionChange = 1;

        if (player.weapons[player.currentWeapon].isWeaponLocked)
        {
            return;
        }

        int weaponToSwitch = player.currentWeapon;

        if (weaponToSwitch + weaponDirectionChange < player.weapons.Count)
            if (player.weapons[player.currentWeapon + weaponDirectionChange].isWeaponLocked)
                weaponDirectionChange *= 2;        

        if (weaponToSwitch + weaponDirectionChange >= player.weapons.Count)
            weaponToSwitch = 0;        
        else
            weaponToSwitch += weaponDirectionChange;                

        player.SwitchWeapon(weaponToSwitch, false);
    }

    /// <summary>
    /// Fire the weapon, the shot type based on whether the weapon has a projectile prefab or not (instant, ray cast fire or projectile instantiate + force)
    /// </summary>
    public void Shoot()
    {
        
        if (player.curMana >= manaShotCost)
        {

            if (weaponFirePrefab != null)
            {                
                Destroy(Instantiate(weaponFirePrefab, shootPosition.transform.position, shootPosition.transform.rotation), 2f);
            }
            else if (weaponFire != null)
            {
                weaponFire.Play();
            }

            
            Debug.Log("Zap: " + canFire);
            

            weaponAnimator.Play("Shoot", -1, 0f);
            player.curMana -= manaShotCost;

            // if the staff has a projectile prefab attached, do a projectile attack
            if (projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, shootPosition.transform.position, shootPosition.transform.rotation) as GameObject;
                projectile.GetComponent<Rigidbody>().AddForce(shootPosition.transform.forward * (projectileSpeed * 50));
                projectile.GetComponent<BallProjectile>().damage = damage;

                StartCoroutine(RemoveProjectile(projectile, range / 10f));
            }
            else // otherwise do a raycast instant attack
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
                {
                    Debug.Log("Hit: " + hit.transform.name);


                    Target destructable = hit.transform.GetComponent<Target>();
                    Enemy enemy = hit.transform.GetComponent<Enemy>();

                    if (destructable != null)
                    {
                        destructable.TakeDamage(damage);
                    }

                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                        //enemy.
                    }
                                        

                    GameObject impactTemp = Instantiate(impactFX, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactTemp, 2f);
                }
            }
           
        }

    }

    /// <summary>
    /// Removes the projectile from the game scene after specified delay time
    /// </summary>
    /// <param name="projectile">the projectile gameobject to remove</param>
    /// <param name="delayTime">the delay time before the object will be removed</param>
    /// <returns></returns>
    IEnumerator RemoveProjectile(GameObject projectile, float delayTime)
    {
        //wait for a few seconds before we destroy the arrow prefab
        yield return new WaitForSeconds(delayTime);
        //destroy bullet so we don't build up too many instances of the bullet
        Destroy(projectile);
    }

    /// <summary>
    /// Reloads the weapons mana stores (ammo) and reduces players overall currently stored mana (total available ammo)
    /// </summary> 
    public void Reload()
    {   
        if (player.storedMana > player.maxMana)
        {
            player.storedMana -= player.maxMana - player.curMana;
            player.curMana = player.maxMana;            
        }
        else
        {            
            player.curMana = player.storedMana;
            player.storedMana = 0;
        }
    }


    /// <summary>
    /// Animation event for recharging staff after a shot is fired
    /// </summary>
    public void Recharge()
    {
        if (staffRechargeParticles != null)
        {
            staffRechargeParticles.Play();
        }
    }
}


