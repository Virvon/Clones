using UnityEngine;

namespace Clones.StateMachine
{
    public class MiningTransition : AttackTransition
    {
        protected override bool IsTargetsEnter(int overlapCount)
        {
            for(var i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out Enemy enemy))
                    return false;
            }

            return base.IsTargetsEnter(overlapCount);
        }

        protected override bool IsRequiredTarget(IDamageable iDamageble) => iDamageble is PreyResource ? true : false;
    }
}
