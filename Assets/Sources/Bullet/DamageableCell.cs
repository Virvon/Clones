using UnityEngine;

public class DamageableCell
{
    public IDamageable Damageable { get; private set; }
    public Vector3 ForceDirection { get; private set; }

    public DamageableCell(IDamageable damageable, Vector3 forceDirection)
    {
        Damageable = damageable;

        forceDirection.z = 0;
        ForceDirection = forceDirection.normalized;
    }
}
