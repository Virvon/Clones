using Clones.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SplashBullet : Bullet
{
    [SerializeField] private DestroyTimer _destroyTimer;

    private SplashBulletData _bulletData;
    private Vector3 _direction;
    private GameObject _selfObject;
    private readonly Collider[] _overlapColliders = new Collider[64];
    private bool _isCollisioned = false;
    
    public override BulletStaticData BulletData => _bulletData;

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

        if (other.TryGetComponent(out IDamageable damageable) && damageable != _selfObject.GetComponent<IDamageable>())
        {
            if (_selfObject.TryGetComponent(out Enemy enemy) && damageable is Enemy)
                return;

            _isCollisioned = true;

            List<DamageableCell> damageableCells = new List<DamageableCell>();
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _bulletData.Radius, _overlapColliders);

            for (var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out IDamageable damageable1) && damageable1 != _selfObject.GetComponent<IDamageable>())
                    damageableCells.Add(new DamageableCell(damageable1, ((MonoBehaviour)damageable1).transform.position - _selfObject.transform.position));
            }

            s_Hitted?.Invoke(damageableCells);
            Hitted?.Invoke();

            _destroyTimer.Destroy();
        }
    }

    public override void Shoot(IDamageable targetDamageable, GameObject selfObject, Transform shootPoint, Action<List<DamageableCell>> Hitted)
    {
        if (targetDamageable.IsAlive)
            _direction = (((MonoBehaviour)targetDamageable).transform.position + new Vector3(0, 1, 0)) - shootPoint.transform.position;
        else
            _direction = shootPoint.transform.forward;

        transform.position = shootPoint.transform.position;

        transform.rotation = Quaternion.LookRotation(_direction);
    
        _selfObject = selfObject;
        s_Hitted = Hitted;

        Shooted?.Invoke();
    }

    public override void Init(BulletStaticData bulletData) => _bulletData = (SplashBulletData)bulletData;


    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_bulletData.LifeTime);

        Destroy(gameObject);
    }
}
