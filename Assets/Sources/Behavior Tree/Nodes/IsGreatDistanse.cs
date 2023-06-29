using BehaviorDesigner.Runtime.Tasks;

namespace Clones.BehaviorTree
{
    public class IsGreatDistanse : Conditional
    {
        public SharedCharacter SelfCharacter;
        public SharedEnemy Enemy;

        public SharedNavMeshAgent NavMeshAgent;

        private TargetArea _targetArea => Enemy.Value.TargetArea;
        private Character _target => Enemy.Value.Target;
        private float _distance => NavMeshAgent.Value.stoppingDistance;

        public override TaskStatus OnUpdate()
        {
            if((SelfCharacter.Value.transform.position - _target.transform.position).magnitude < _distance)
                return TaskStatus.Failure;

            return (SelfCharacter.Value.transform.position - _targetArea.transform.position).magnitude > _distance ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
