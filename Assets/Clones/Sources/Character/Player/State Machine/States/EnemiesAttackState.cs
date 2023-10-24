using UnityEngine;

namespace Clones.StateMachine
{
    public class EnemiesAttackState : AttackState
    {
        protected override bool IsRequiredTarget(IDamageable iDamageble) => 
            iDamageble is EnemyHealth;
    }
}
