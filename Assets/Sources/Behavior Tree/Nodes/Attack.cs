using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Clones.BehaviorTree
{
    public class Attack : Action
    {
        public SharedCharacterAttack SharedCharacterAttack;
        public SharedEnemy Enemy;

        private Character _target => Enemy.Value.Target;

        public override TaskStatus OnUpdate()
        {
            SharedCharacterAttack.Value.TryAttack(_target);
            RotateTo(_target.transform.position);
            return TaskStatus.Running;
        }

        private void RotateTo(Vector3 target)
        {
            transform.rotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
        }
    }
}
