using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Clones.BehaviorTree
{
    public class MoveToTarget : Action
    {
        public SharedEnemy Enemy;
        public SharedFloat TargetRadius;
        public SharedEnemyAnimationSwithcer _animationSwithcer;

        private NavMeshAgent _agent;
        private Vector3 _targetPoint;

        private GameObject Target => Enemy.Value.Target;
        private float DistanceToTarget => (Enemy.Value.transform.position - Target.transform.position).magnitude;

        public override void OnStart()
        {
            _targetPoint = Enemy.Value.transform.position;
            _agent = Enemy.Value.GetComponent<NavMeshAgent>();
            _agent.isStopped = false;

            _animationSwithcer.Value.SetMovement(true);
        }

        public override TaskStatus OnUpdate()
        {
            if (DistanceToTarget > TargetRadius.Value + _agent.stoppingDistance)
            {
                if ((Target.transform.position - _targetPoint).magnitude > TargetRadius.Value)
                    _targetPoint = GetPointInTargetRadius();
            }
            else if (_targetPoint != Target.transform.position)
            {
                _targetPoint = Target.transform.position;
            }

            _agent.SetDestination(_targetPoint);
            Debug.DrawLine(Enemy.Value.transform.position, _targetPoint);

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            if (_agent.isOnNavMesh)
            {
                _agent.isStopped = true;
                _animationSwithcer.Value.SetMovement(false);
            }
        }

        private Vector3 GetPointInTargetRadius()
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(Random.insideUnitSphere * TargetRadius.Value + Target.transform.position, out hit, TargetRadius.Value, NavMesh.AllAreas);

            return hit.position;
        }
    }
}
