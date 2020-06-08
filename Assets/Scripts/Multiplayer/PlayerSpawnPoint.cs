
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    //Only relevant for CTF maps, shoul be 0 or 1 (2 teams)
    [SerializeField] private int teamId = 0;
    private void Awake()
    {
        PlayerSpawnSystem.AddSpawnPoint(transform);
    }

    private void OnDestroy()
    {
        PlayerSpawnSystem.RemoveSpawnPoint(transform);
    }

    private void OnDrawGizmos()
    {
        if (teamId == 0)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, .3f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2f);
        }else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, .3f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2f);
        }
       
    }
}
