using System;
using UnityEngine;

public class FactionResources : MonoBehaviour
{
    public int energy;
    public int influence;
    public event Action<int, int> OnResourcesChanged;

    public void AddEnergy(int v) { energy += v; OnResourcesChanged?.Invoke(energy, influence); }
    public void AddInfluence(int v) { influence += v; OnResourcesChanged?.Invoke(energy, influence); }
    public bool HasEnergy(int cost) => energy >= cost;
}