using UnityEngine;

namespace Clones.StateMachine
{
    public class EnemiesAttackTransition : AttackTransition
    {
        protected override bool IsRequiredTarget(IDamageble iDamageble) => iDamageble is Enemy ? true : false;
    }
}
