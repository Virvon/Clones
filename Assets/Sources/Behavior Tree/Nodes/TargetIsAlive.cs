using BehaviorDesigner.Runtime.Tasks;

namespace Clones.BehaviorTree
{
    public class TargetIsAlive : Conditional
    {
        public SharedEnemy Enemy;

        private bool _isAlive;

        public override void OnAwake()
        {
            _isAlive = true;
            Enemy.Value.Target.Died += OnDied;
        }

        public override TaskStatus OnUpdate() => _isAlive ? TaskStatus.Success : TaskStatus.Failure;

        private void OnDied(IDamageble damageble)
        {
            _isAlive = false;
            Enemy.Value.Target.Died -= OnDied;
        }
    }
}
