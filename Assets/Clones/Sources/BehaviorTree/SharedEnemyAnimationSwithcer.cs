using System;
using BehaviorDesigner.Runtime;
using Clones.Animation;

namespace Clones.BehaviorTree
{
    [Serializable]
    public class SharedEnemyAnimationSwithcer : SharedVariable<EnemyAnimationSwitcher>
    {
        public static implicit operator SharedEnemyAnimationSwithcer(EnemyAnimationSwitcher value) => new SharedEnemyAnimationSwithcer { Value = value};
    }
}
