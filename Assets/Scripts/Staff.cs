using System.Collections;
using UnityEngine;

public class Staff : MonoBehaviour
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
    public PlayerHandler player;

    [SerializeField]
    private ParticleSystem weaponFire;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    public GameObject shootPosition;


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
                GameObject projectile = Instantiate(projectilePrefab, shootPosition.transform.position, shootPosition.transform.rotation) as GameObject;
                projectile.GetComponent<Rigidbody>().AddForce(shootPosition.transform.forward * (projectileSpeed * 50));
                

                StartCoroutine(RemoveProjectile(projectile, range / 10f));
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

    IEnumerator RemoveProjectile(GameObject projectile, float delayTime)
    {
        //wait for a few seconds before we destroy the arrow prefab
        yield return new WaitForSeconds(delayTime);
        //destroy bullet so we don't build up too many instances of the bullet
        Destroy(projectile);
    }

    public void Reload()
    {
        //weaponReload.Play();
        player.curMana = player.maxMana;
    }
}


