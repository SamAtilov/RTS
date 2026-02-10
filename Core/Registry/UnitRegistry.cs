using System.Collections.Generic;
using UnityEngine;

public class UnitRegistry : MonoBehaviour
{
    public static UnitRegistry Instance { get; private set; }

    Dictionary<int, Selectable> unitsById = new();
    Dictionary<string, List<Selectable>> unitsByFaction = new();

    void Awake()
    {
        Instance = this;
    }

    public void Register(Selectable unit)
    {
        if (unit == null)
            return;

        // Регистрируем по ID
        unitsById[unit.Id] = unit;

        // Регистрируем по фракции
        FactionMember fm = unit.GetComponent<FactionMember>();
        if (fm == null || fm.Faction == null)
            return;

        string factionId = fm.FactionId;

        if (!unitsByFaction.ContainsKey(factionId))
            unitsByFaction[factionId] = new List<Selectable>();

        unitsByFaction[factionId].Add(unit);
    }

    public void Unregister(Selectable unit)
    {
        if (unit == null)
            return;

        unitsById.Remove(unit.Id);

        FactionMember fm = unit.GetComponent<FactionMember>();
        if (fm == null || fm.Faction == null)
            return;

        string factionId = fm.FactionId;

        if (unitsByFaction.TryGetValue(factionId, out var list))
        {
            list.Remove(unit);
        }
    }

    public Selectable GetById(int id)
    {
        unitsById.TryGetValue(id, out var unit);
        return unit;
    }

    public List<Selectable> GetUnitsByFaction(string factionId)
    {
        if (unitsByFaction.TryGetValue(factionId, out var list))
            return list;

        return new List<Selectable>();
    }
}