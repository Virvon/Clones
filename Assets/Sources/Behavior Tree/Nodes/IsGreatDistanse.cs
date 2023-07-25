 using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Clones.BehaviorTree
{
    public class IsGreatDistanse : Conditional
    {
        public SharedEnemy Enemy;

        public SharedNavMeshAgent NavMeshAgent;

        private PlayerArea _playerArea => Enemy.Value.TargetArea;
        private Player _player => Enemy.Value.Target;
        private float _distance => NavMeshAgent.Value.stoppingDistance;
        private float _stoppingDistance;

        public override void OnStart() => _stoppingDistance = NavMeshAgent.Value.stoppingDistance;

        public override TaskStatus OnUpdate()
        {
            if ((Enemy.Value.transform.position - _playerArea.transform.position).magnitude <= _distance || (Enemy.Value.transform.position - _player.transform.position).magnitude <= _distance) 
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, _player.transform.position - transform.position, out hit, _distance))
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

            return TaskStatus.Success;
        }
    }
}
