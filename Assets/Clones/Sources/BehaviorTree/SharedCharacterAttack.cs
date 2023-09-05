using System;
using BehaviorDesigner.Runtime;


namespace Clones.BehaviorTree
{
    [Serializable]
    public class SharedCharacterAttack : SharedVariable<global::CharacterAttack>
    {
        public static implicit operator SharedCharacterAttack(global::CharacterAttack value) => new SharedCharacterAttack { Value = value};
    }
}
