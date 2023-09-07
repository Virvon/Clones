using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New SingleBullet", menuName = "Data/Bullet/Create new single bullet", order = 51)]
    public class SingleBulletData : BulletData
    {
        [SerializeField] private SingleBullet _bulletPrefab;
        [SerializeField] private float _force;
        [SerializeField] private float _lifeTime;

        public float Force => _force;
        public float LifeTime => _lifeTime;
        public override Bullet BulletPrefab => _bulletPrefab;
    }
}
