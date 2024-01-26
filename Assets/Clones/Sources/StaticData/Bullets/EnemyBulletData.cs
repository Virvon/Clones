using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New EnemyBullet", menuName = "Data/Bullet/Create new enemy bullet", order = 51)]
    public class EnemyBulletData : BulletStaticData
    {
        [SerializeField] private EnemyBullet _bulletPrefab;
        [SerializeField] private float _force;
        [SerializeField] private float _lifeTime;

        public float Force => _force;
        public float LifeTime => _lifeTime;
        public override Bullet BulletPrefab => _bulletPrefab;
    }
}
