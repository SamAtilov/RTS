using UnityEngine;

public class AirUnitMovement : UnitMovementBase
{
    protected override void Move()
    {
        Vector3 dir = target - transform.position;

        if (dir.magnitude <= stopDistance)
        {
            hasTarget = false;
            IsMoving = false;
            return;
        }

        dir.Normalize();

        transform.forward = Vector3.Lerp(transform.forward, dir, 5f * Time.deltaTime);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        IsMoving = true;
    }
}