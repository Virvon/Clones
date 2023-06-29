using UnityEngine;

public interface IDamageble
{
    public void TakeDamage(float damage);

    public Vector3 Position { get; }
}
