using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Bullet : MonoBehaviour
{
    public abstract event Action Hitted;

    protected abstract event Action<List<DamageableCell>> s_Hitted;

    public abstract void Shoot(IDamageable targetDamageable, IDamageable selfDamageable, Transform shootPoint, Action<List<DamageableCell>> Hitted = null);
}
