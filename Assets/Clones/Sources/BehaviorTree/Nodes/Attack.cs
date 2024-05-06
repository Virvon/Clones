using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Clones.BehaviorTree
{
    public class Attack : Action
    {
        public SharedCharacterAttack SharedCharacterAttack;
        public SharedEnemy Enemy;

        private GameObject Target => Enemy.Value.Target;

        public override TaskStatus OnUpdate()
        {
            SharedCharacterAttack.Value.TryAttack(Target.GetComponent<IDamageable>());

            RotateTo(Target.transform.position);

            return TaskStatus.Running;
        }

        private void RotateTo(Vector3 targetPosition)
        {
            targetPosition.y = transform.position.y;

            var direction = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, 1080 * Time.deltaTime);
        }
    }
}
