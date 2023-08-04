using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New TopDownBullet", menuName = "Data/Bullet/Create new top down bullet", order = 51)]
    public class TopDownBulletData : BulletData
    {
        [SerializeField] private TopDownBullet _bulletPrefab;
        [SerializeField] private float _upOffset;
        [SerializeField] private float _horizontalOffset;
        [SerializeField] private float _lifeTime;

        public float UpOffset => _upOffset;
        public float HorizontalOffset => _horizontalOffset;
        public float LifeTime => _lifeTime;
        public override Bullet BulletPrefab => _bulletPrefab;
    }
}
