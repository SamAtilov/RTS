using UnityEngine;

public class UnitFaction : MonoBehaviour
{
    public Faction faction;

    public bool IsEnemy(UnitFaction other)
    {
        return other != null && faction != other.faction;
    }
}