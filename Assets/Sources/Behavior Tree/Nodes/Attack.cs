using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Clones.BehaviorTree
{
    public class Attack : Action
    {
        public SharedCharacterAttack SharedCharacterAttack;
        public SharedEnemy Enemy;

        private Clone _target => Enemy.Value.Target;

        public override TaskStatus OnUpdate()
        {
            SharedCharacterAttack.Value.TryAttack(_target);
            RotateTo(_target.transform.position);
            return TaskStatus.Running;
        }

        private void RotateTo(Vector3 target)
        {
            var direction = Quaternion.LookRotation(target - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, 1080 * Time.deltaTime);
        }
    }
}
