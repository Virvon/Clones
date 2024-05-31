using Clones.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBullet : HittableBullet
{
    public IDamageable HitTarget { get; private set; }

    public override BulletStaticData BulletData => _bulletData;

    private EnemyBulletData _bulletData;
    private Vector3 _direction;
    private bool _isCollisioned = false;

    public override event Action Hitted;
    public override event Action Shooted;
    protected override event Action<List<DamageableKnockbackInfo>> DamageableHitted;

    public override void Init(BulletStaticData bulletData) =>
        _bulletData = (EnemyBulletData)bulletData;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = _direction.normalized * _bulletData.Force;

        StartCoroutine(LifiTimer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isCollisioned)
            return;

        if (other.TryGetComponent(out IDamageable damageable) && other.TryGetComponent(out Enemy _) == false)
        {
            _isCollisioned = true;
            HitTarget = damageable;

            DamageableHitted?.Invoke(new List<DamageableKnockbackInfo> { new DamageableKnockbackInfo(damageable, Vector3.zero) });
            Hitted?.Invoke();
            Destroy(gameObject);
        }
    }

    public override void Shoot(IDamageable targetDamageable, GameObject selfObject, Transform shootPoint, Action<List<DamageableKnockbackInfo>> Hitted)
    {
        if (targetDamageable.IsAlive)
            _direction = (((MonoBehaviour)targetDamageable).transform.position + new Vector3(0, 1.8f, 0)) - shootPoint.transform.position;
        else
            _direction = shootPoint.transform.forward;

        transform.position = shootPoint.transform.position;

        transform.rotation = Quaternion.LookRotation(_direction);

        DamageableHitted = Hitted;

        Shooted?.Invoke();
    }

    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_bulletData.LifeTime);

        Destroy(gameObject);
    }
}