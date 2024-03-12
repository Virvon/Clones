using Clones.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SingleBullet : Bullet
{
    [SerializeField] private DestroyTimer _destoryTimer;

    private SingleBulletData _bulletData;
    private Vector3 _direction;
    private GameObject _selfObject;
    private bool _isCollisioned = false;

    public IDamageable HitTarget { get; private set; }
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
            HitTarget = damageable;

            Vector3 knockbakcDirection = other.transform.position - _selfObject.transform.position;

            s_Hitted?.Invoke(new List<DamageableCell> { new DamageableCell(damageable, knockbakcDirection) });
            Hitted?.Invoke();

            _destoryTimer.Destroy();
        }
    }

    public override void Init(BulletStaticData bulletData) => 
        _bulletData = (SingleBulletData)bulletData;

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

    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_bulletData.LifeTime);

        Destroy(gameObject);
    }
}
