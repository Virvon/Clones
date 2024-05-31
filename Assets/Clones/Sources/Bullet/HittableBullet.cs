using System;

namespace Clones.BulletSystem
{
    public abstract class HittableBullet : Bullet
    {
        public abstract event Action Hitted;
    }
}