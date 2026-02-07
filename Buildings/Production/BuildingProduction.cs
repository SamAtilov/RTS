using System.Collections.Generic;
using UnityEngine;

public class BuildingProduction : MonoBehaviour
{
    public Transform spawnPoint;
    public List<GameObject> unitPrefabs;

    FactionMember faction;
    FactionResources resources;

    void Awake()
    {
        faction = GetComponent<FactionMember>();
    }

    void Start()
    {
        resources = FactionManager.Instance.GetResources(faction.factionId);
    }

    public void Produce(int index, int cost)
    {
        if (resources.energy < cost) return;

        resources.energy -= cost;

        GameObject unit = Instantiate(
            unitPrefabs[index],
            spawnPoint.position,
            spawnPoint.rotation
        );

        unit.GetComponent<FactionMember>().factionId = faction.factionId;
    }
}