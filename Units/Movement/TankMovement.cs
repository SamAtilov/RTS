using UnityEngine;

public class TankMovement : UnitMovementBase
{
    public float turnSpeed = 60f;

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

        float angle = Vector3.SignedAngle(transform.forward, dir, Vector3.up);

        if (Mathf.Abs(angle) > 5f)
        {
            transform.Rotate(0, Mathf.Sign(angle) * turnSpeed * Time.deltaTime, 0);
            IsMoving = false;
            return;
        }

        CollisionAvoidance avoidance = GetComponent<CollisionAvoidance>();

        Vector3 finalDir = dir;

        if (avoidance != null)
            finalDir = avoidance.GetAvoidance(dir);

        transform.position += finalDir * moveSpeed * Time.deltaTime;
        IsMoving = true;
    }
}