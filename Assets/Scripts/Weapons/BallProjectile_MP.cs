
using UnityEngine;
using Mirror;

/// <summary>
/// Script for a Projectile collision
/// </summary>
public class BallProjectile_MP : NetworkBehaviour   
{
    public float damage = 5;
    public GameObject impactFX;
    public GameObject impactDecal;
    public float destroyAfter = 10;
    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        Target destructable = other.transform.GetComponent<Target>();
        Enemy enemy = other.transform.GetComponent<Enemy>();

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
            GameObject impactTemp = Instantiate(impactFX, transform.position, Quaternion.identity);
            Destroy(impactTemp, 2f);
        }
        else
        {
            Debug.Log("Impact FX is not attached to " + this.name);
        }

        if (impactDecal != null)
        {
            GameObject impactTemp = Instantiate(impactDecal, transform.position, Quaternion.identity);
            //Destroy(impactTemp, 360f);
        }
        else
        {
            Debug.Log("Impact FX is not attached to " + this.name);
        }

        Destroy(gameObject);
    }

}
