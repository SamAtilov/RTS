using UnityEngine;

public class UnitRegistry : MonoBehaviour
{
    FactionMember faction;

    void Awake()
    {
        faction = GetComponent<FactionMember>();
    }

    void Start()
    {
        FactionManager.Instance.RegisterUnit(faction.factionId, gameObject);
    }

    void OnDestroy()
    {
        if (FactionManager.Instance != null)
            FactionManager.Instance.UnregisterUnit(faction.factionId, gameObject);
    }
}