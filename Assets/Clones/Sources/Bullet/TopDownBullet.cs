using Clones.Character;
using Clones.Character.Enemy;
using Clones.StaticData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Clones.BulletSystem
{
    [RequireComponent(typeof(SphereCollider))]
    public class TopDownBullet : Bullet
    {
        public Transform Muzzle { get; private set; }
        public Vector3 TargetPosition { get; private set; }
        public float UpOffset => _bulletData.UpOffset;
        public override BulletStaticData BulletData => _bulletData;

        private TopDownBulletData _bulletData;
        private GameObject _selfObject;
        private SphereCollider _collider;

        public override event Action Shooted;
        protected override event Action<List<DamageableKnockbackInfo>> DamageableHitted;

        public override void Init(BulletStaticData bulletData) =>
            _bulletData = (TopDownBulletData)bulletData;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && damageable != _selfObject.GetComponent<IDamageable>())
            {
                if (damageable is Enemy)
                    return;

                Vector3 knockbackDirection = other.transform.position - transform.position;

                DamageableHitted?.Invoke(new List<DamageableKnockbackInfo> { new DamageableKnockbackInfo(damageable, knockbackDirection) });
            }
        }

        public override void Shoot(IDamageable targetDamageable, GameObject selfObject, Transform shootPoint = null, Action<List<DamageableKnockbackInfo>> Hitted = null)
        {
            _collider = GetComponent<SphereCollider>();
            _collider.center = new Vector3(0, UpOffset, 0);

            DamageableHitted = Hitted;
            Muzzle = shootPoint;

            if (targetDamageable.IsAlive)
                TargetPosition = new Vector3(Random.Range(-_bulletData.HorizontalOffset, _bulletData.HorizontalOffset) + ((MonoBehaviour)targetDamageable).transform.position.x, ((MonoBehaviour)targetDamageable).transform.position.y, Random.Range(-_bulletData.HorizontalOffset, _bulletData.HorizontalOffset) + ((MonoBehaviour)targetDamageable).transform.position.z);

            transform.position = TargetPosition;
            _selfObject = selfObject;

            Shooted?.Invoke();

            StartCoroutine(Shooter());
            StartCoroutine(LifiTimer());
        }

        private IEnumerator LifiTimer()
        {
            yield return new WaitForSeconds(_bulletData.LifeTime);

            Destroy(gameObject);
        }

        private IEnumerator Shooter()
        {
            float time = 0;
            Vector3 startColliderCenter = _collider.center;

            while (_collider.center != Vector3.zero)
            {
                time += Time.deltaTime;

                _collider.center = Vector3.Lerp(startColliderCenter, Vector3.zero, time / _bulletData.ShootTime);

                yield return null;
            }
        }
    }
}