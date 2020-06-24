using Mirror;
using UnityEngine;

public class PlayerShootingController : NetworkBehaviour
{
    [SyncVar]
    public float health;
    public float fireRate = 0.25f;
    private float nextFire;

    private void Update()
    {
        Camera fpsCamera = Camera.main;
        if (fpsCamera.enabled == false)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            Debug.Log("Fire");
            nextFire = Time.time + fireRate;
            Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, fpsCamera.transform.forward, out hit))
            {
                Debug.Log("Hit");
                PlayerShootingController playershooter = hit.collider.GetComponent<PlayerShootingController>();
                if (playershooter != null)
                {
                    Debug.Log("Update Taking Damage");
                    playershooter.TakeDamage(20f);
                }
                
            }
        }
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Take Damage");
        if (isServer)
        {
            RpcTakeDamage(damage);
        }
        else
        {
            CmdTakeDamage(damage);
            Debug.Log("score: " + damage);
        }
    }
    [ClientRpc]
    void RpcTakeDamage(float damage)
    {
        Debug.Log("RPC Taking Damage");
        this.health -= damage;
    }

    [Command]
    void CmdTakeDamage(float damage)
    {
        Debug.Log("cmd Taking Damage");
        TakeDamage(damage);
    }
}
