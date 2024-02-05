using System;
using BehaviorDesigner.Runtime;


namespace Clones.BehaviorTree
{
    [Serializable]
    public class SharedCharacterAttack : SharedVariable<CharacterAttack>
    {
        public static implicit operator SharedCharacterAttack(CharacterAttack value) => new SharedCharacterAttack { Value = value};
    }
}
