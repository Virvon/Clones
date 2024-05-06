using System;

public abstract class HittableBullet : Bullet
{
    public abstract event Action Hitted;
}
