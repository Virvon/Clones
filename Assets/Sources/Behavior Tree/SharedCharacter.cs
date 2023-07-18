using System;
using BehaviorDesigner.Runtime;

namespace Clones.BehaviorTree
{
    [Serializable]
    public class SharedCharacter : SharedVariable<Clone>
    {
        public static implicit operator SharedCharacter(Clone value) => new SharedCharacter { Value = value};
    }
}
