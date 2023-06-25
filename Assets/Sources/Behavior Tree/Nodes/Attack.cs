using BehaviorDesigner.Runtime.Tasks;

namespace Clones.BehaviorTree
{
    public class Attack : Action
    {
        public SharedCharacterAttack SharedCharacterAttack;

        public override TaskStatus OnUpdate()
        {
            SharedCharacterAttack.Value.Attack();

            return TaskStatus.Success;
        }
    }
}
