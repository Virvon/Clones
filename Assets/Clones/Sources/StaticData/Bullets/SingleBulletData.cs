using Clones.BulletSystem;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New SingleBullet", menuName = "Data/Bullet/Create new single bullet", order = 51)]
    public class SingleBulletData : BulletStaticData
    {
        [SerializeField] private SingleBullet _bulletPrefab;

        public float Force;
        public float LifeTime;

        public override Bullet BulletPrefab => _bulletPrefab;
    }
}
