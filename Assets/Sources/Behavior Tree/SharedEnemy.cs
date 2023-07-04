using System;
using BehaviorDesigner.Runtime;

namespace Clones.BehaviorTree
{
    [Serializable]
    public class SharedEnemy : SharedVariable<Enemy>
    {
        public static implicit operator SharedEnemy(Enemy value) => new SharedEnemy { Value = value};
    }
}
