using UnityEngine;

public class RTSInput : MonoBehaviour
{
    public LayerMask groundMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Если клик по юниту
                Health targetHealth = hit.collider.GetComponentInParent<Health>();
                UnitFaction targetFaction = hit.collider.GetComponentInParent<UnitFaction>();

                foreach (var unit in SelectionManager.Instance.SelectedUnits)
                {
                    UnitFaction uf = unit.GetComponent<UnitFaction>();
                    UnitAttack attack = unit.GetComponent<UnitAttack>();
                    IMovable move = unit.GetComponent<IMovable>();

                    if (targetHealth != null &&
                        targetFaction != null &&
                        uf.IsEnemy(targetFaction) &&
                        attack != null)
                    {
                        attack.SetTarget(targetHealth);
                    }
                    else if (move != null)
                    {
                        move.MoveTo(hit.point);
                    }
                }
            }
        }
    }
}