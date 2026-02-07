using System;
using System.Collections;
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

    public Queue<UnitBlueprint> queue = new();
    public event Action OnQueueChanged;

    IEnumerator BuildRoutine(UnitBlueprint blueprint)
    {
        float t = blueprint.buildTime;
        while (t > 0f) { t -= Time.deltaTime; yield return null; }
        Instantiate(blueprint.prefab, spawnPoint.position, spawnPoint.rotation);
        OnQueueChanged?.Invoke();
        StartNextIfAny();
    }

    void StartNextIfAny()
    {
        if (queue.Count > 0)
            StartCoroutine(BuildRoutine(queue.Dequeue()));
    }
}