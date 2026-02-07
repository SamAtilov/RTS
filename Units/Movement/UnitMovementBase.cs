using UnityEngine;

public abstract class UnitMovementBase : MonoBehaviour, IMovable
{
    public float moveSpeed = 5f;
    public float stopDistance = 0.1f;

    protected Vector3 target;
    protected bool hasTarget;

    public bool IsMoving { get; protected set; }

    public virtual void MoveTo(Vector3 point)
    {
        target = point;
        target.y = transform.position.y;
        hasTarget = true;
    }

    void Update()
    {
        if (!hasTarget)
        {
            IsMoving = false;
            return;
        }

        Move();
    }

    protected abstract void Move();
}