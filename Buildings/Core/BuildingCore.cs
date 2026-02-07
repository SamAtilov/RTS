using UnityEngine;

public enum BuildingType
{
    HQ,
    Generator,
    Factory,
    Lab
}

public class BuildingCore : MonoBehaviour
{
    public BuildingType type;
    public int factionId;

    public bool IsOperational { get; private set; } = true;

    void Start()
    {
        // позже тут будет проверка энергии
        IsOperational = true;
    }
}