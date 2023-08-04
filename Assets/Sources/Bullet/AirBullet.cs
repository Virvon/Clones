using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AirBullet : Bullet
{
    [SerializeField] private float _upOffset;
    [SerializeField] private float _horizontalOffset;
    [SerializeField] private float _lifeTime;

    public Transform Muzzle { get; private set; }
    public Vector3 TargetPosition { get; private set; }
    public float UpOffset => _upOffset;

    private IDamageable _selfDamageable;

    public override event Action Hitted;
    protected override event Action<List<DamageableCell>> s_Hitted;


    public override void Shoot(IDamageable targetDamageable, IDamageable selfDamageable, Transform shootPoint = null, Action<List<DamageableCell>> Hitted = null)
    {
        s_Hitted = Hitted;
        Muzzle = shootPoint;
        TargetPosition = new Vector3(Random.Range(-_horizontalOffset, _horizontalOffset) + ((MonoBehaviour)targetDamageable).transform.position.x, ((MonoBehaviour)targetDamageable).transform.position.y, Random.Range(-_horizontalOffset, _horizontalOffset) + ((MonoBehaviour)targetDamageable).transform.position.z);
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

            Vector3 forceDirection = other.transform.position - transform.position;

            s_Hitted?.Invoke(new List<DamageableCell> { new DamageableCell(damageable, forceDirection) });
        }
    }

    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
