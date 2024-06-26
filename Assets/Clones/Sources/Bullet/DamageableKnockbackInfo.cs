﻿using Clones.Character;
using UnityEngine;

namespace Clones.BulletSystem
{
    public class DamageableKnockbackInfo
    {
        public DamageableKnockbackInfo(IDamageable damageable, Vector3 knockbackDirection)
        {
            Damageable = damageable;

            KnockbackDirection = knockbackDirection.normalized;
            KnockbackDirection = new Vector3(knockbackDirection.x, 0, knockbackDirection.z);
        }

        public IDamageable Damageable { get; private set; }
        public Vector3 KnockbackDirection { get; private set; }
    }
}