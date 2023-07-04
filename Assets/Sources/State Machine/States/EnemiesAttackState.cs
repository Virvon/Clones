namespace Clones.StateMachine
{
    public class EnemiesAttackState : AttackState
    {
        protected override bool IsRequiredTarget(IDamageble iDamageble) => iDamageble is Enemy ? true : false;
    }
}
