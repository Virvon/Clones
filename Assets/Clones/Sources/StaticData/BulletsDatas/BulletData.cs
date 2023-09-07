using UnityEngine;

namespace Clones.Data
{
    public abstract class BulletData : ScriptableObject
    {
        [SerializeField] private GameObject _bulletProjectile;
        [SerializeField] private GameObject _bulletHitEffect;
        [SerializeField] private GameObject _bulletMuzzle;

        public GameObject BulletProjectile => _bulletProjectile;
        public GameObject BulletHitEffect => _bulletHitEffect;
        public GameObject BulletMuzzle => _bulletMuzzle;

        public abstract Bullet BulletPrefab { get; }
    }
}
