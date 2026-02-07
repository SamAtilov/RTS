using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    float current;

    void Awake()
    {
        current = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        current -= dmg;

        if (current <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}