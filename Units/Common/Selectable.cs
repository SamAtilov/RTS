using UnityEngine;

public class Selectable : MonoBehaviour
{
    SelectionRing ring;
    FactionMember faction;

    void Awake()
    {
        ring = GetComponentInChildren<SelectionRing>(true);
        faction = GetComponent<FactionMember>();
    }

    public void Select()
    {
        ring.Show(Color.green);
    }

    public void Deselect()
    {
        ring.Hide();
    }

    public void ShowEnemy()
    {
        ring.Show(Color.red);
    }
}