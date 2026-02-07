using UnityEngine;

public class GroundUnitMovement : UnitMovementBase
{
    public float rotationSpeed = 720f;

    protected override void Move()
    {
        Vector3 dir = target - transform.position;
        dir.y = 0f;

        float distance = dir.magnitude;

        if (distance <= stopDistance)
        {
            hasTarget = false;
            IsMoving = false;
            return;
        }

        dir.Normalize();

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );

        CollisionAvoidance avoidance = GetComponent<CollisionAvoidance>();

        Vector3 finalDir = dir;

        if (avoidance != null)
            finalDir = avoidance.GetAvoidance(dir);

        transform.position += finalDir * moveSpeed * Time.deltaTime;
        IsMoving = true;
    }
}