using BehaviorDesigner.Runtime.Tasks;
using Clones.Character.Player;
using UnityEngine;

namespace Clones.BehaviorTree
{
    public class IsGreatDistanse : Conditional
    {
        public SharedEnemy Enemy;
        public SharedNavMeshAgent NavMeshAgent;

        private float _stoppingDistance;

        private GameObject Target => Enemy.Value.Target;
        private float Distance => NavMeshAgent.Value.stoppingDistance;

        public override void OnStart() => 
            _stoppingDistance = NavMeshAgent.Value.stoppingDistance;

        public override TaskStatus OnUpdate()
        {
            float distanceToPlayer = (Enemy.Value.transform.position - Target.transform.position).magnitude;

            if (distanceToPlayer <= Distance)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, Target.transform.position - transform.position, out hit, Distance))
                {
                    if (hit.collider.TryGetComponent(out Player player))
                    {
                        NavMeshAgent.Value.stoppingDistance = _stoppingDistance;
                        return TaskStatus.Failure;
                    }
                    else
                    {
                        NavMeshAgent.Value.stoppingDistance -= 0.1f;
                    }
                }
            }
            else
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
    }
}
