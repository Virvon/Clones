using System;
using BehaviorDesigner.Runtime;

namespace Clones.BehaviorTree
{
    [Serializable]
    public class SharedEnemyStats : SharedVariable<Stats>
    {
        public static implicit operator SharedEnemyStats(Enemy value) => new SharedEnemyStats { Value = value.Stats};
    }
}
