using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Clones.BehaviorTree
{
    public class MoveToTarget : Action
    {
        public SharedCharacter SelfCharacter;
        public SharedEnemy Enemy;
        public SharedFloat TargetRadius;

        private NavMeshAgent _agent;
        private Vector3 _targetPoint;
        private Character _target => Enemy.Value.Target;
        private float _distanceToTarget => (SelfCharacter.Value.transform.position - _target.transform.position).magnitude;

        public override void OnStart()
        {
            _targetPoint = SelfCharacter.Value.transform.position;
            _agent = SelfCharacter.Value.GetComponent<NavMeshAgent>();
            _agent.isStopped = false;
        }

        public override TaskStatus OnUpdate()
        {
            if (_distanceToTarget > TargetRadius.Value + _agent.stoppingDistance)
            {
                if ((_target.transform.position - _targetPoint).magnitude > TargetRadius.Value)
                    _targetPoint = GetPointInTargetRadius();
            }
            else if (_targetPoint != _target.transform.position)
            {
                _targetPoint = _target.transform.position;
            }

            _agent.SetDestination(_targetPoint);
            Debug.DrawLine(SelfCharacter.Value.transform.position, _targetPoint);

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            _agent.isStopped = true;
        }

        private Vector3 GetPointInTargetRadius()
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(Random.insideUnitSphere * TargetRadius.Value + _target.transform.position, out hit, TargetRadius.Value, NavMesh.AllAreas);

            return hit.position;
        }
    }
}
