using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Clones.BehaviorTree
{
    public class IsLessDistanse : Conditional
    {
        public SharedCharacter SelfCharacter;
        public SharedCharacter TargetCharacter;

        public SharedFloat Distance;

        public override TaskStatus OnUpdate() => (SelfCharacter.Value.transform.position - TargetCharacter.Value.transform.position).magnitude >= Distance.Value ? TaskStatus.Failure : TaskStatus.Success;
    }
}
