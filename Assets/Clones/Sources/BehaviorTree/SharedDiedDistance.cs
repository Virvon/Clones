using System;
using BehaviorDesigner.Runtime;

namespace Clones.BehaviorTree
{
    [Serializable]
    public class SharedDiedDistance : SharedVariable<int>
    {
        public static implicit operator SharedDiedDistance(int value) => new SharedDiedDistance { Value = value };
    }
}
