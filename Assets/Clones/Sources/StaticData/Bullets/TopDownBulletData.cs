﻿using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New TopDownBullet", menuName = "Data/Bullet/Create new top down bullet", order = 51)]
    public class TopDownBulletData : BulletStaticData
    {
        [SerializeField] private TopDownBullet _bulletPrefab;
        [SerializeField] private float _upOffset;
        [SerializeField] private float _horizontalOffset;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _shootTime;

        public float UpOffset => _upOffset;
        public float HorizontalOffset => _horizontalOffset;
        public float LifeTime => _lifeTime;
        public float ShootTime => _shootTime;
        public override Bullet BulletPrefab => _bulletPrefab;
    }
}
