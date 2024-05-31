using Clones.BulletSystem;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New SplashBullet", menuName = "Data/Bullet/Create new splash bullet", order = 51)]
    public class SplashBulletData : BulletStaticData
    {
        [SerializeField] private SplashBullet _bulletPrefab;
        [SerializeField] private float _force;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _radius;

        public float Force => _force;
        public float LifeTime => _lifeTime;
        public float Radius => _radius;
        public override Bullet BulletPrefab => _bulletPrefab;
    }
}
