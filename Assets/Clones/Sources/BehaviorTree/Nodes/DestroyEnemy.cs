using BehaviorDesigner.Runtime.Tasks;

namespace Clones.BehaviorTree
{
    public class DestroyEnemy : Action
    {
        public SharedEnemyHealth EnemyHealth;

        public override void OnStart() => 
            EnemyHealth.Value.Disappear();
    }
}
