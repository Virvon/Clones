using Clones.Types;
using UnityEngine;

namespace Clones.Data
{
    public abstract class BulletStaticData : ScriptableObject
    {
        public BulletType Type;
        public GameObject BulletProjectile;
        public GameObject BulletHitEffect;
        public GameObject BulletMuzzle;

        public abstract Bullet BulletPrefab { get; }
    }
}
