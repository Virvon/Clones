using Clones.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public abstract BulletStaticData BulletData { get; }

    public abstract event Action Hitted;
    public abstract event Action Shooted;

    protected abstract event Action<List<DamageableCell>> s_Hitted;

    public abstract void Shoot(IDamageable targetDamageable, GameObject selfObject, Transform shootPoint, Action<List<DamageableCell>> Hitted = null);

    public abstract void Init(BulletStaticData bulletData);
}
