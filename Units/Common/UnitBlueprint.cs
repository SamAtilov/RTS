using UnityEngine;

[CreateAssetMenu(menuName = "RTS/Unit Blueprint")]
public class UnitBlueprint : ScriptableObject
{
    public GameObject prefab;
    public int energyCost;
    public float buildTime;
}