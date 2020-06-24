using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactDetect2 : MonoBehaviour
{
    public Renderer meshRenderer;
    public Material instanceMaterial;
    void Start()
    {
        instanceMaterial = meshRenderer.material;
    }
   
    public void SetHitPosition(Vector3 hitPoint)
    {
        Vector3 hitVector = gameObject.transform.InverseTransformPoint(hitPoint);
        instanceMaterial.SetVector("_HitPosition", hitVector);
    }
}
