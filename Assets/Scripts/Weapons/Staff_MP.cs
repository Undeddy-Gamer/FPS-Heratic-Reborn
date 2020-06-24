using System.Collections;
using UnityEngine;
using Mirror;

/// <summary>
/// Multiplayer version of the Staff script (not currently working) i beleive it has something to do with it being a child object of the component that has the network identity?
/// </summary>
public class Staff_MP : NetworkBehaviour
{
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
    private ParticleSystem weaponFirePrefab;
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
            //The animation has a trigger for when to apply the reload
            weaponAnimator.Play("StaffReload",-1,0f);
        }
    }


    /// <summary>
    /// Fire the weapon, the shot type based on whether the weapon has a projectile prefab or not (instant ray cast fire or projectile instantiate + force)
    /// </summary>
    public void Shoot()
    {
        if (player.curMana >= manaShotCost)
        {

            weaponFire.Play();

            Debug.Log("Zap: " + canFire);            

            weaponAnimator.Play("Shoot", -1, 0f);
            player.curMana -= manaShotCost;


            if (projectilePrefab != null)
            {
                if (isServer)
                {
                    RpcSpawnProjectile();
                }
                else
                { 
                    CmdFireProjectile();
                }
                //StartCoroutine(RemoveProjectile(projectile, range / 10f));
            }
            else
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
    /// Multiplayer spawn projectile work in progress
    /// </summary>
    [ClientRpc]
    void RpcSpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPosition.transform.position, shootPosition.transform.rotation) as GameObject;

        projectile.GetComponent<Rigidbody>().AddForce(shootPosition.transform.forward * (projectileSpeed * 50));
        projectile.GetComponent<BallProjectile>().damage = damage;

        NetworkServer.Spawn(projectile);
    }
    /// <summary>
    /// Multiplayer spawn projectile work in progress
    /// </summary>
    [Command]
    void CmdFireProjectile()
    {
        Shoot();
    }
        
    /// <summary>
    /// Reload command for animation trigger (probably the only code that is required on the model the above should be moved to the weapon script)
    /// </summary>
    public void Reload()
    {
        //weaponReload.Play();
        player.curMana = player.maxMana;
    }
}


