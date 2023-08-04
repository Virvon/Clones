using System;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : Bullet
{
    public override event Action Hitted;
    protected override event Action<List<IDamageable>> s_Hitted;

    public override void Shoot(IDamageable targetDamageable, IDamageable selfDamageable, Transform shootPoint, Action<List<IDamageable>> Hitted = null)
    {
        throw new NotImplementedException();
    }
}
