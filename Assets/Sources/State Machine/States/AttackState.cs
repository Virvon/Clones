using UnityEngine;

namespace Clones.StateMachine
{
    public class AttackState : State
    {
        [SerializeField] private float _attackRadius;
        [SerializeField] private CharacterAttack _characterAttack;

        private readonly Collider[] _overlapColliders = new Collider[64];

        private void Update() => Attack();

        private void Attack()
        {
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _overlapColliders);

            for(var i = 0; i < overlapCount; i++)
            {
                if(_overlapColliders[i].TryGetComponent(out IDamageble idamageble) && _overlapColliders[i].TryGetComponent(out Enemy enemy))
                {
                    _characterAttack.TryAttack(enemy);
                    RotateTo(enemy.transform.position);
                    break;
                }
            }
        }

        private void RotateTo(Vector3 target)
        {
            transform.rotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRadius);
        }
    }
}
