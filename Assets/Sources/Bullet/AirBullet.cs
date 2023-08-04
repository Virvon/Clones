using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBullet : Bullet
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _lifeTime;

    public Transform Muzzle { get; private set; }
    public Vector3 TargetPosition { get; private set; }
    public Vector3 Offset => _offset;

    private IDamageable _selfDamageable;

    public override event Action Hitted;
    protected override event Action<List<IDamageable>> s_Hitted;

    public override void Shoot(IDamageable targetDamageable, IDamageable selfDamageable, Transform shootPoint = null, Action<List<IDamageable>> Hitted = null)
    {
        s_Hitted = Hitted;
        Muzzle = shootPoint;
        TargetPosition = ((MonoBehaviour)targetDamageable).transform.position;
        transform.position = TargetPosition;
        _selfDamageable = selfDamageable;

        StartCoroutine(LifiTimer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable) && damageable != _selfDamageable)
        {
            if (_selfDamageable is Enemy && damageable is Enemy)
                return;

            s_Hitted?.Invoke(new List<IDamageable> { damageable });
        }
    }

    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
