using UnityEngine;

public class FactionResources : MonoBehaviour
{
    public int energy;
    public int influence;

    public void AddEnergy(int value)
    {
        energy += value;
    }

    public void AddInfluence(int value)
    {
        influence += value;
    }

    public bool HasEnergy(int cost)
    {
        return energy >= cost;
    }
}