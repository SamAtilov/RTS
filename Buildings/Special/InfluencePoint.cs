using UnityEngine;

public class InfluencePoint : MonoBehaviour
{
    public int influencePerTick = 1;
    public float tickTime = 3f;

    int controllingFaction = -1;
    FactionResources resources;

    void Start()
    {
        resources = FindObjectOfType<FactionResources>();
        InvokeRepeating(nameof(GiveInfluence), tickTime, tickTime);
    }

    public void Capture(int factionId)
    {
        controllingFaction = factionId;
    }

    void GiveInfluence()
    {
        if (controllingFaction != -1)
            resources.AddInfluence(influencePerTick);
    }
}