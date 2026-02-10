using UnityEngine;

public class SelectionIndicator : MonoBehaviour
{
    public Renderer ring;

    FactionMember faction;

    void Awake()
    {
        faction = GetComponentInParent<FactionMember>();
    }

    public void ShowSelected()
    {
        ring.material.color = faction.Faction.factionColor;
        ring.enabled = true;
    }

    public void Hide()
    {
        ring.enabled = false;
    }
}