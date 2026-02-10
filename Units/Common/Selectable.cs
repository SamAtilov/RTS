using UnityEngine;

public class Selectable : MonoBehaviour
{
    static int nextId = 1;

    public int Id { get; private set; }

    SelectionRing ring;
    FactionMember faction;

    void Awake()
    {
        Id = nextId++;

        ring = GetComponentInChildren<SelectionRing>(true);
        faction = GetComponent<FactionMember>();

        if (UnitRegistry.Instance != null)
            UnitRegistry.Instance.Register(this);
    }

    void OnDestroy()
    {
        if (UnitRegistry.Instance != null)
            UnitRegistry.Instance.Unregister(this);
    }

    public void Select()
    {
        if (ring != null)
            ring.Show(Color.green);
    }

    public void Deselect()
    {
        if (ring != null)
            ring.Hide();
    }

    public void ShowEnemy()
    {
        if (ring != null)
            ring.Show(Color.red);
    }
}