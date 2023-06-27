using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Clones.BehaviorTree
{
    public class IsLessDistanse : Conditional
    {
        public SharedCharacter SelfCharacter;
        public SharedTargetArea TargetArea;

        public SharedFloat Distance;

        public override TaskStatus OnUpdate() => (SelfCharacter.Value.transform.position - TargetArea.Value.transform.position).magnitude < Distance.Value ? TaskStatus.Success : TaskStatus.Failure;
    }
}
