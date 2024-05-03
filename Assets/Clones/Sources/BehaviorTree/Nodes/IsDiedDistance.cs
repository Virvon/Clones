using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Clones.BehaviorTree
{
    public class IsDiedDistance : Conditional
    {
        public SharedEnemy Enemy;
        public SharedDiedDistance DiedDistance;

        private GameObject Target => Enemy.Value.Target;

        public override TaskStatus OnUpdate()
        {
            if (Vector3.Distance(Target.transform.position, Enemy.Value.transform.position) > DiedDistance.Value)
                return TaskStatus.Success;

            return TaskStatus.Failure;
        }
    }
}
