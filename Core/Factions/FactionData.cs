using UnityEngine;

[CreateAssetMenu(menuName = "RTS/Faction")]
public class FactionData : ScriptableObject
{
    public string factionId;   // "imperium", "orks"
    public string displayName;

    public Color factionColor;
}