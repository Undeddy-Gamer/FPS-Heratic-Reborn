
using UnityEngine;
using UnityEngine.InputSystem;

public class Staff : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    // the Higher the faster
    public float rateOfFire = 15f;

    public Camera fpsCam;
    public ParticleSystem fizz;
    public GameObject impactFX;

    private float canFire = 0f;
    public void Shoot(InputAction.CallbackContext context)
    {
        if (Time.time >= canFire)
        {
            canFire = Time.time + 1f / rateOfFire;

            fizz.Play();
            Debug.Log("Shoot");
            RaycastHit hit;

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
            
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                //GameObject impactTemp = Instantiate(impactFX, hit.point, Quaternion.LookRotation(hit.normal));
                //Destroy(impactTemp, 2f);
            }
        }
    }
}
