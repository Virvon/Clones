using UnityEngine;

public class DamageableCell
{
    public IDamageable Damageable { get; private set; }
    public Vector3 KnockbackDirection { get; private set; }

    public DamageableCell(IDamageable damageable, Vector3 knockbackDirection)
    {
        Damageable = damageable;

        KnockbackDirection = knockbackDirection.normalized;
        KnockbackDirection = new Vector3(knockbackDirection.x, 0, knockbackDirection.z);
    }
}
