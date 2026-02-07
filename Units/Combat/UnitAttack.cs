using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    public float range = 10f;
    public float fireRate = 1f;
    public int damage = 10;

    float cooldown;
    Health target;
    FactionMember faction;

    void Awake()
    {
        faction = GetComponent<FactionMember>();
    }

    public void SetTarget(Health h)
    {
        if (h == null) return;

        var otherFaction = h.GetComponent<FactionMember>();
        if (otherFaction != null && otherFaction.factionId == faction.factionId)
            return;

        target = h;
    }

    void Update()
    {
        if (target == null) return;

        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist > range) return;

        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            target.TakeDamage(damage);
            cooldown = 1f / fireRate;
        }
    }
}