
using UnityEngine;

public class Staff : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    // the Higher the faster
    public float rateOfFire = 15f;

    //the camera of the player to use for raycast hit detection
    public Camera cam;

    // particle for staff weapon when fired (instead of a muzzle flash we might have magic flash)
    public ParticleSystem fizz;
    // a prefab for where the weapon hits
    public GameObject impactFX;

    private float canFire = 0f;


    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= canFire)
        {
            canFire = Time.time + 1f / rateOfFire;
            Shoot();
        }
    }
    public void Shoot()
    {
       

            fizz.Play();
            Debug.Log("Zap: " + canFire);
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Debug.Log("Hit: " + hit.transform.name);
            
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                GameObject impactTemp = Instantiate(impactFX, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactTemp, 2f);
            }
        
    }
}
