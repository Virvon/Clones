using Clones.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SphereCollider))]
public class TopDownBullet : Bullet
{
    public Transform Muzzle { get; private set; }
    public Vector3 TargetPosition { get; private set; }
    public float UpOffset => _bulletData.UpOffset;
    public override BulletData BulletData => _bulletData;

    private TopDownBulletData _bulletData;
    private IDamageable _selfDamageable;
    private SphereCollider _collider;

    public override event Action Hitted;
    protected override event Action<List<DamageableCell>> s_Hitted;
    public override event Action Shooted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable) && damageable != _selfDamageable)
        {
            if (_selfDamageable is Enemy && damageable is Enemy)
                return;

            Vector3 knockbackDirection = other.transform.position - transform.position;

            s_Hitted?.Invoke(new List<DamageableCell> { new DamageableCell(damageable, knockbackDirection) });
        }
    }

    public override void Shoot(IDamageable targetDamageable, GameObject _selfObject, Transform shootPoint = null, Action<List<DamageableCell>> Hitted = null)
    {
        _collider = GetComponent<SphereCollider>();
        _collider.center = new Vector3(0, UpOffset, 0);

        s_Hitted = Hitted;
        Muzzle = shootPoint;

        if (targetDamageable.IsAlive)
            TargetPosition = new Vector3(Random.Range(-_bulletData.HorizontalOffset, _bulletData.HorizontalOffset) + ((MonoBehaviour)targetDamageable).transform.position.x, ((MonoBehaviour)targetDamageable).transform.position.y, Random.Range(-_bulletData.HorizontalOffset, _bulletData.HorizontalOffset) + ((MonoBehaviour)targetDamageable).transform.position.z);
        //else
            //TargetPosition = ((MonoBehaviour)_selfObject).transform.position + ((MonoBehaviour)_selfObject).transform.forward * 3;

        transform.position = TargetPosition;
        //_selfDamageable = _selfObject;

        Shooted?.Invoke();

        StartCoroutine(Shooter());
        StartCoroutine(LifiTimer());
    }

    public override void Init(BulletData bulletData) => _bulletData = (TopDownBulletData)bulletData;

    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_bulletData.LifeTime);

        Destroy(gameObject);
    }

    private IEnumerator Shooter()
    {
        float time = 0;
        Vector3 startColliderCenter = _collider.center;

        while(_collider.center != Vector3.zero)
        {
            time += Time.deltaTime;

            _collider.center = Vector3.Lerp(startColliderCenter, Vector3.zero, time / _bulletData.ShootTime);

            yield return null;
        }
    }
}
