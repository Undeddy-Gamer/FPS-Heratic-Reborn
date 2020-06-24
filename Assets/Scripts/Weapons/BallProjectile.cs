using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for a Projectile collision
/// </summary>
public class BallProjectile : MonoBehaviour   
{
    public float damage = 5;
    public GameObject impactFX;
    public GameObject impactDecal;

    private void OnCollisionEnter(Collision collision)
    {
        Target destructable = collision.transform.GetComponent<Target>();
        Enemy enemy = collision.transform.GetComponent<Enemy>();

        if (destructable != null)
        {
            destructable.TakeDamage(damage);
        }

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
        if (impactFX != null)
        {

            //Quaternion.FromToRotation(Vector3.forward, other.normal);
            GameObject impactTemp = Instantiate(impactFX, transform.position, Quaternion.identity);
            Destroy(impactTemp, 2f);
        }
        else
        {
            Debug.Log("Impact FX is not attached to " + this.name);
        }

        if (impactDecal != null)
        {
            if (collision.collider.gameObject.tag != "Player")
            { 
                ContactPoint contact = collision.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                GameObject impactTemp = Instantiate(impactDecal, transform.position, rot);
                //Destroy(impactTemp, 360f);
            }

        }
        else
        {
            Debug.Log("Impact Decal is not attached to " + this.name);
        }

        Destroy(gameObject);
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    Target destructable = other.transform.GetComponent<Target>();
    //    Enemy enemy = other.transform.GetComponent<Enemy>();

    //    if (destructable != null)
    //    {
    //        destructable.TakeDamage(damage);
    //    }

    //    if (enemy != null)
    //    {
    //        enemy.TakeDamage(damage);            
    //    }

    //    if (impactFX != null)
    //    {
            
    //        //Quaternion.FromToRotation(Vector3.forward, other.normal);
    //        GameObject impactTemp = Instantiate(impactFX, transform.position, Quaternion.identity);
    //        Destroy(impactTemp, 2f);
    //    }
    //    else
    //    {
    //        Debug.Log("Impact FX is not attached to " + this.name);
    //    }

    //    if (impactDecal != null)
    //    {
    //        GameObject impactTemp = Instantiate(impactDecal, transform.position, Quaternion.identity);
    //        //Destroy(impactTemp, 360f);
    //    }
    //    else
    //    {
    //        Debug.Log("Impact FX is not attached to " + this.name);
    //    }

    //    Destroy(gameObject);
    //}

}
