using Clones.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SplashBullet : Bullet
{
    public override BulletData BulletData => _bulletData;

    private SplashBulletData _bulletData;
    private Vector3 _direction;
    private IDamageable _selfDamageable;
    private readonly Collider[] _overlapColliders = new Collider[64];
    private bool _isCollisioned = false;

    public override event Action Hitted;
    protected override event Action<List<DamageableCell>> s_Hitted;
    public override event Action Shooted;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = _direction.normalized * _bulletData.Force;

        StartCoroutine(LifiTimer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isCollisioned)
            return;

        if (other.TryGetComponent(out IDamageable damageable) && damageable != _selfDamageable)
        {
            if (_selfDamageable is Enemy && damageable is Enemy)
                return;

            _isCollisioned = true;

            List<DamageableCell> damageableCells = new List<DamageableCell>();
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _bulletData.Radius, _overlapColliders);

            for (var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out IDamageable damageable1) && damageable1 != _selfDamageable)
                    damageableCells.Add(new DamageableCell(damageable1, ((MonoBehaviour)damageable1).transform.position - transform.position));
            }

            s_Hitted?.Invoke(damageableCells);
            Hitted?.Invoke();
            Destroy(gameObject);
        }
    }

    public override void Shoot(IDamageable targetDamageable, IDamageable selfDamageable, Transform shootPoint, Action<List<DamageableCell>> Hitted)
    {
        _direction = ((MonoBehaviour)targetDamageable).transform.position - shootPoint.transform.position;
        transform.position = shootPoint.transform.position;

        transform.rotation = Quaternion.LookRotation(_direction);
    
        _selfDamageable = selfDamageable;
        s_Hitted = Hitted;

        Shooted?.Invoke();
    }

    public override void Init(BulletData bulletData) => _bulletData = (SplashBulletData)bulletData;


    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_bulletData.LifeTime);

        Destroy(gameObject);
    }
}
