using System;
using BehaviorDesigner.Runtime;

namespace Clones.BehaviorTree
{
    [Serializable]
    public class SharedCharacter : SharedVariable<Character>
    {
        public static implicit operator SharedCharacter(Character value) => new SharedCharacter { Value = value};
    }
}
