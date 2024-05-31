using Clones.Character;
using Clones.Character.Enemy;
using Clones.Environment;

namespace Clones.StateMachine
{
    public class MiningTransition : AttackTransition
    {
        protected override bool IsTargetsEnter(int overlapCount)
        {
            for (var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out Enemy _))
                    return false;
            }

            return base.IsTargetsEnter(overlapCount);
        }

        protected override bool IsRequiredTarget(IDamageable iDamageble) => 
            iDamageble is PreyResource;
    }
}