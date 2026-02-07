using System.Collections.Generic;
using UnityEngine;

public static class CommandSystem
{
    public static void Move(IEnumerable<Selectable> units, Vector3 point)
    {
        foreach (var u in units)
            u.GetComponent<IMovable>()?.MoveTo(point);
    }

    public static void Attack(IEnumerable<Selectable> units, Health target)
    {
        foreach (var u in units)
            u.GetComponent<UnitAttack>()?.SetTarget(target);
    }
}