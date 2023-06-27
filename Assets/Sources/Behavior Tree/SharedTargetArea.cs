using System;
using BehaviorDesigner.Runtime;

namespace Clones.BehaviorTree
{
    [Serializable]
    public class SharedTargetArea : SharedVariable<TargetArea>
    {
        public static implicit operator SharedTargetArea(TargetArea value) => new SharedTargetArea { Value = value };
    }
}
