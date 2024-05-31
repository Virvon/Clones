using Clones.Character;
using Clones.Character.Enemy;

namespace Clones.StateMachine
{
    public class EnemiesAttackState : AttackState
    {
        protected override bool IsRequiredTarget(IDamageable iDamageble) => 
            iDamageble is EnemyHealth;
    }
}
