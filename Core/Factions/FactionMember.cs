using UnityEngine;

public class FactionMember : MonoBehaviour
{
    [SerializeField] private FactionData faction;

    public FactionData Faction => faction;
    public string FactionId => faction != null ? faction.factionId : string.Empty;

    public void SetFaction(FactionData data)
    {
        faction = data;
    }
}