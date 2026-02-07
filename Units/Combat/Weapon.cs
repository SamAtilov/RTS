using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range = 10f;
    public float damage = 10f;
    public float fireRate = 1f;

    float nextFireTime;

    public bool CanFire => Time.time >= nextFireTime;

    public void Fire(Health target)
    {
        if (!CanFire || target == null) return;

        nextFireTime = Time.time + 1f / fireRate;
        target.TakeDamage(damage);
    }
}