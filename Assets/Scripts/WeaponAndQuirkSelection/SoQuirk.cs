using UnityEngine;

[CreateAssetMenu]
public class SoQuirk : ScriptableObject
{
    public string quirkName;
    public string description;
    public float maxHealthPercentage;
    public float healOverTimePercentage;
    public float runSpeedPercentage;
    public float jumpHeightPercentage;
    public float rateOfFirePercentage;
    public float damagePercentage;
    public float accuracyPercentage;
    public float reloadSpeedPercentage;

    public Sprite Icon;
    public ParticleSystem quirkParticleVisual;
    public Material quirkMaterialVisual;
}
