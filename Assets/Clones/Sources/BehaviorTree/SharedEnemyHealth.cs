using System;
using BehaviorDesigner.Runtime;
using Clones.Character.Enemy;

namespace Clones.BehaviorTree
{
    [Serializable]
    public class SharedEnemyHealth : SharedVariable<EnemyHealth>
    {
        public static implicit operator SharedEnemyHealth(EnemyHealth value) => new SharedEnemyHealth { Value = value };
    }
}