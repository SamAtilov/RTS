using System.Collections.Generic;
using UnityEngine;

public class FactionManager : MonoBehaviour
{
    public static FactionManager Instance { get; private set; }

    Dictionary<int, FactionResources> resources = new();
    Dictionary<int, HashSet<GameObject>> units = new();
    Dictionary<string, FactionResources> resourcesByFaction;

    void Awake()
    {
        Instance = this;
    }

    public void RegisterFaction(int id, FactionResources res)
    {
        resources[id] = res;
        units[id] = new HashSet<GameObject>();
    }

    public FactionResources GetResources(string factionId)
    {
        return resourcesByFaction[factionId];
    }

    public void RegisterUnit(int factionId, GameObject unit)
    {
        units[factionId].Add(unit);
    }

    public void UnregisterUnit(int factionId, GameObject unit)
    {
        units[factionId].Remove(unit);
    }

    public IEnumerable<GameObject> GetUnits(int factionId)
    {
        return units[factionId];
    }
}