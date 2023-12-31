namespace Clones.StateMachine
{
    public class EnemiesAttackTransition : AttackTransition
    {
        protected override bool IsRequiredTarget(IDamageable iDamageble) => 
            iDamageble is EnemyHealth;
    }
}
