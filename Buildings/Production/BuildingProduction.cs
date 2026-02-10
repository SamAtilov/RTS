using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProduction : MonoBehaviour
{
    [Header("Spawn")]
    public Transform spawnPoint;

    [Header("Units")]
    public List<GameObject> unitPrefabs;

    FactionMember factionMember;
    FactionResources resources;

    public Queue<UnitBlueprint> queue = new();
    public event Action OnQueueChanged;

    void Awake()
    {
        factionMember = GetComponent<FactionMember>();
    }

    void Start()
    {
        resources = FactionManager.Instance
            .GetResources(factionMember.FactionId);
    }

    public void Produce(int index, int cost)
    {
        if (index < 0 || index >= unitPrefabs.Count)
            return;

        if (resources.energy < cost)
            return;

        resources.energy -= cost;

        GameObject unit = Instantiate(
            unitPrefabs[index],
            spawnPoint.position,
            spawnPoint.rotation
        );

        // ✅ КЛЮЧЕВОЕ МЕСТО
        unit.GetComponent<FactionMember>()
            .SetFaction(factionMember.Faction);
    }

    IEnumerator BuildRoutine(UnitBlueprint blueprint)
    {
        float t = blueprint.buildTime;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            yield return null;
        }

        GameObject unit = Instantiate(
            blueprint.prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        unit.GetComponent<FactionMember>()
            .SetFaction(factionMember.Faction);

        OnQueueChanged?.Invoke();
        StartNextIfAny();
    }

    void StartNextIfAny()
    {
        if (queue.Count > 0)
            StartCoroutine(BuildRoutine(queue.Dequeue()));
    }
}