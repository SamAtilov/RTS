using UnityEngine;

public class PlayerFactionController : MonoBehaviour
{
    public static PlayerFactionController Instance { get; private set; }

    public FactionData playerFaction;

    void Awake()
    {
        Instance = this;
    }
}