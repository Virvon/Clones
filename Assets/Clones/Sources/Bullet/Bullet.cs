using Clones.Character;
using Clones.StaticData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.BulletSystem
{
    public abstract class Bullet : MonoBehaviour
    {
        public abstract event Action Shooted;
        protected abstract event Action<List<DamageableKnockbackInfo>> DamageableHitted;

        public abstract BulletStaticData BulletData { get; }

        public abstract void Init(BulletStaticData bulletData);
        public abstract void Shoot(IDamageable targetDamageable, GameObject selfObject, Transform shootPoint, Action<List<DamageableKnockbackInfo>> Hitted = null);
    }
}