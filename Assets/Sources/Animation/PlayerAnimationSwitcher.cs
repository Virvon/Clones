using UnityEngine;
using System.Collections;

namespace Clones.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationSwitcher : MonoBehaviour
    {
        [SerializeField] private CharacterAttack _characterAttack;
        [SerializeField] private Player _player; 
        [SerializeField] private DirectionHandler _directionHandler;

        private float _movementSpeed;
        private float _movementAnimationSpeed => _player.MovementSpeed / _movementSpeed;

        private Animator _animator;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _movementSpeed = _player.MovementSpeed;

            _characterAttack.AttackStarted += OnAttackStarted;

            _directionHandler.Activated += OnMove;
            _directionHandler.Deactivated += OnStop;

            _player.MovementStats.MovementSpeedChanged += OnMovementSpeedChanged;
        }

        private void OnDisable()
        {
            _characterAttack.AttackStarted -= OnAttackStarted;

            _directionHandler.Activated += OnMove;
            _directionHandler.Deactivated += OnStop;

            _player.MovementStats.MovementSpeedChanged += OnMovementSpeedChanged;
        }

        private void OnAttackStarted() => _animator.SetTrigger(Animations.Player.Triggers.Attack);

        private void OnMove() => _animator.SetBool(Animations.Player.Bools.IsMoved, true);

        private void OnStop() => _animator.SetBool(Animations.Player.Bools.IsMoved, false);

        private void OnMovementSpeedChanged() => _animator.SetFloat(Animations.Player.Floats.MovementAnimationSpeed, _movementAnimationSpeed);
    }
}
