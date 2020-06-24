using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Burn Impact Effect
/// </summary>
public class BurnIntoObject : MonoBehaviour
{

    private float _burnAmount = 1;
    private Material _mat;

    void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }

    /// <summary>
    /// reduces the burn amount over time until only the base impact image is left
    /// </summary>
    void Update()
    {
        if (_burnAmount > 0)
        {
            _burnAmount -= Time.deltaTime;

            if (_burnAmount < 0)
            {
                _burnAmount = 0;
            }

            _mat.SetFloat("_BurnAmount", _burnAmount);
        }
    }


    
}
