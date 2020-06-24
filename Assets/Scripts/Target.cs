
using UnityEngine;
/// <summary>
/// Used to set an object as damageable and destroyable
/// </summary>
public class Target : MonoBehaviour
{
    public float health = 100f;
   
    public void TakeDamage(float amount)
    {
        health -= amount;

        if ( health <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
