using BehaviorDesigner.Runtime.Tasks;

namespace Clones.BehaviorTree
{
    public class Attack : Action
    {
        public SharedCharacterAttack SharedCharacterAttack;

        public override TaskStatus OnUpdate()
        {
            SharedCharacterAttack.Value.TryAttack();
            return TaskStatus.Running;
        }
    }
}
