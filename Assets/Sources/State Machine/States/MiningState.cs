namespace Clones.StateMachine
{
    public class MiningState : AttackState
    {
        protected override bool IsRequiredTarget(IDamageble iDamageble) => iDamageble is MiningFacility ? true : false;
    }
}
