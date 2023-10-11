 using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Clones.BehaviorTree
{
    public class IsGreatDistanse : Conditional
    {
        public SharedEnemy Enemy;

        public SharedNavMeshAgent NavMeshAgent;

        private GameObject _target => Enemy.Value.Target;
        private float _distance => NavMeshAgent.Value.stoppingDistance;
        private float _stoppingDistance;

        public override void OnStart() => 
            _stoppingDistance = NavMeshAgent.Value.stoppingDistance;

        public override TaskStatus OnUpdate()
        {
            float distanceToPlayer = (Enemy.Value.transform.position - _target.transform.position).magnitude;

            if (distanceToPlayer <= _distance)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, _target.transform.position - transform.position, out hit, _distance))
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
