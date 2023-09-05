using UnityEngine;

namespace Clones.StateMachine
{
    public class EnemiesAttackTransition : AttackTransition
    {
        protected override bool IsRequiredTarget(IDamageable iDamageble) => iDamageble is Enemy ? true : false;
    }
}
