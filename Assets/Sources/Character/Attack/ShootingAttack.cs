using System;
using UnityEngine;

public class ShootingAttack : CharacterAttack
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootingPoint;

    private event Action<IDamageable> Hitted;

    private Vector3 _targetPosition => ((MonoBehaviour)Target).transform.position;

    private void OnEnable() => Hitted += MakeDamage;

    private void OnDisable() => Hitted -= MakeDamage;

    protected override void Attack()
    {
        Bullet bullet = Instantiate(_bullet, _shootingPoint.transform.position, Quaternion.identity);
        bullet.Shoot(_targetPosition - transform.position, (IDamageable)Attackble, Hitted);
    }

    private void MakeDamage(IDamageable damageble)
    {
        //if (((MonoBehaviour)Target).TryGetComponent(out Rigidbody rigidbody))
        //{
        //    Vector3 force = ((MonoBehaviour)damageble).transform.position - ((MonoBehaviour)Attackble).transform.position;
        //    force.y = 0;
        //    rigidbody.AddForce(force.normalized * Attackble.KnockbackForce);

        //    Debug.Log("knockbacked");
        //}

        damageble.TakeDamage(Attackble.Damage);
    }
}
