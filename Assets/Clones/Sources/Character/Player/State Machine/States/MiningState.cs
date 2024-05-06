namespace Clones.StateMachine
{
    public class MiningState : AttackState
    {
        protected override bool IsRequiredTarget(IDamageable iDamageble) => 
            iDamageble is PreyResource;
    }
}