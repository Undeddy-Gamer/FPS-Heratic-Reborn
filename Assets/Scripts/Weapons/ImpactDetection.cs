using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Impact detection for walls
/// </summary>
public class ImpactDetection : MonoBehaviour
{

    private float _hitTime;
    private Material _mat;

    void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hitTime > 0)
        {
            _hitTime -= Time.deltaTime * 1000;

            if (_hitTime < 0)
            {
                _hitTime = 0;
            }

            _mat.SetFloat("_HitTime", _hitTime);
        }
    }


    //detect collision against floor/wall
    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            _mat.SetVector("_HitPosition", transform.InverseTransformPoint(contact.point));
            _hitTime = 500;
            _mat.SetFloat("_HitTime", _hitTime);
        }
    }
}
