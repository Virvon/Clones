using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Clones.BehaviorTree
{
    public class MoveToTarget : Action
    {
        public SharedCharacter SelfCharacter;
        public SharedCharacter TargetCharacter;
        public SharedFloat TargetRadius;

        private NavMeshAgent _agent;
        private Vector3 _targetPoint;
        private float _distanceToTarget => (SelfCharacter.Value.transform.position - TargetCharacter.Value.transform.position).magnitude;

        public override void OnStart()
        {
            _targetPoint = SelfCharacter.Value.transform.position;
            _agent = SelfCharacter.Value.GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            if (_distanceToTarget > TargetRadius.Value + _agent.stoppingDistance)
            {
                if ((TargetCharacter.Value.transform.position - _targetPoint).magnitude > TargetRadius.Value)
                    _targetPoint = GetPointInTargetRadius();
            }
            else if (_targetPoint != TargetCharacter.Value.transform.position)
            {
                _targetPoint = TargetCharacter.Value.transform.position;
            }

            _agent.SetDestination(_targetPoint);
            Debug.DrawLine(SelfCharacter.Value.transform.position, _targetPoint);

            return TaskStatus.Running;
        }

        private Vector3 GetPointInTargetRadius()
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(Random.insideUnitSphere * TargetRadius.Value + TargetCharacter.Value.transform.position, out hit, TargetRadius.Value, NavMesh.AllAreas);

            return hit.position;
        }
    }
}
