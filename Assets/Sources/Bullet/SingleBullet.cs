using Clones.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SingleBullet : Bullet
{
    public override BulletData BulletData => _bulletData;

    private SingleBulletData _bulletData;
    private Vector3 _direction;
    private IDamageable _selfDamageable;

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
        if (other.TryGetComponent(out IDamageable damageable) && damageable != _selfDamageable)
        {
            if (_selfDamageable is Enemy && damageable is Enemy)
                return;

            s_Hitted?.Invoke(new List<DamageableCell> { new DamageableCell(damageable, (other.transform.position - transform.position).normalized) });
            Hitted?.Invoke();
            Destroy(gameObject);
        }
    }

    public override void Init(BulletData bulletData) => _bulletData = (SingleBulletData)bulletData;

    public override void Shoot(IDamageable targetDamageable, IDamageable selfDamageable, Transform shootPoint, Action<List<DamageableCell>> Hitted)
    {
        _direction = ((MonoBehaviour)targetDamageable).transform.position - shootPoint.transform.position;
        transform.position = shootPoint.transform.position;

        transform.rotation = Quaternion.LookRotation(_direction);

        _selfDamageable = selfDamageable;
        s_Hitted = Hitted;

        Shooted?.Invoke();
    }

    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_bulletData.LifeTime);

        Destroy(gameObject);
    }

}
