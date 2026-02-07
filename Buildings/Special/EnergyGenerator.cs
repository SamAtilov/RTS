using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    public int energyPerTick = 5;
    public float tickTime = 2f;

    FactionResources resources;

    void Start()
    {
        resources = FindObjectOfType<FactionResources>();
        InvokeRepeating(nameof(Generate), tickTime, tickTime);
    }

    void Generate()
    {
        resources.AddEnergy(energyPerTick);
    }
}