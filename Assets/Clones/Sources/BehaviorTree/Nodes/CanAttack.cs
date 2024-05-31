using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Clones.BehaviorTree
{
    public class CanAttack : Conditional
    {
        public SharedEnemy TargetCharacter;
        public SharedFloat TargetRadius;

        private Vector3 _targetPoint;

        private float Distance => (TargetCharacter.Value.transform.position - _targetPoint).magnitude;

        public override void OnStart()
        {
            _targetPoint = TargetCharacter.Value.transform.position;
        }

        public override TaskStatus OnUpdate()
        {
            if (Distance > TargetRadius.Value)
                return TaskStatus.Failure;
            else
                return TaskStatus.Success;
        }
    }
}
