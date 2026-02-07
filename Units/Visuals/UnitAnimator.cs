using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitAnimator : MonoBehaviour
{
    IMovable movement;
    Animator anim;

    void Awake()
    {
        movement = GetComponent<IMovable>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (movement == null) return;
        anim.SetBool("Run", movement.IsMoving);
    }
}