using Clones.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TopDownBullet : Bullet
{
    public Transform Muzzle { get; private set; }
    public Vector3 TargetPosition { get; private set; }
    public float UpOffset => _bulletData.UpOffset;
    public override BulletData BulletData => _bulletData;

    private TopDownBulletData _bulletData;
    private IDamageable _selfDamageable;

    public override event Action Hitted;
    protected override event Action<List<DamageableCell>> s_Hitted;
    public override event Action Shooted;

    public override void Shoot(IDamageable targetDamageable, IDamageable selfDamageable, Transform shootPoint = null, Action<List<DamageableCell>> Hitted = null)
    {
        s_Hitted = Hitted;
        Muzzle = shootPoint;
        TargetPosition = new Vector3(Random.Range(-_bulletData.HorizontalOffset, _bulletData.HorizontalOffset) + ((MonoBehaviour)targetDamageable).transform.position.x, ((MonoBehaviour)targetDamageable).transform.position.y, Random.Range(-_bulletData.HorizontalOffset, _bulletData.HorizontalOffset) + ((MonoBehaviour)targetDamageable).transform.position.z);
        transform.position = TargetPosition;
        _selfDamageable = selfDamageable;

        Shooted?.Invoke();

        StartCoroutine(LifiTimer());
    }

    public override void Init(BulletData bulletData) => _bulletData = (TopDownBulletData)bulletData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable) && damageable != _selfDamageable)
        {
            if (_selfDamageable is Enemy && damageable is Enemy)
                return;

            Vector3 forceDirection = other.transform.position - transform.position;

            s_Hitted?.Invoke(new List<DamageableCell> { new DamageableCell(damageable, forceDirection) });
        }
    }

    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_bulletData.LifeTime);

        Destroy(gameObject);
    }
}
