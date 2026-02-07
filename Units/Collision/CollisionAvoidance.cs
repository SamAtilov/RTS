using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    public float pushStrength = 5f;

    UnitFootprint footprint;

    void Awake()
    {
        footprint = GetComponent<UnitFootprint>();
    }

    public Vector3 GetAvoidance(Vector3 desiredMove)
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            footprint.radius * 2f
        );

        Vector3 push = Vector3.zero;

        foreach (var hit in hits)
        {
            if (hit.transform == transform) continue;

            UnitFootprint other = hit.GetComponentInParent<UnitFootprint>();
            if (other == null) continue;

            Vector3 dir = transform.position - hit.transform.position;
            dir.y = 0f;

            float dist = dir.magnitude;
            float minDist = footprint.radius + other.radius;

            if (dist < minDist && dist > 0.001f)
            {
                push += dir.normalized * (minDist - dist);
            }
        }

        return (desiredMove + push * pushStrength).normalized;
    }
}