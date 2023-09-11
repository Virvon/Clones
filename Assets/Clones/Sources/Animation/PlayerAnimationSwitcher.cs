using UnityEngine;
using Clones.Infrastructure;
using Clones.StateMachine;

namespace Clones.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationSwitcher : MonoBehaviour
    {
        //[SerializeField] private CharacterAttack _characterAttack;
        [SerializeField] private MovementState _movementState;

        private IInputService _inputService;
        private Animator _animator;

        private float _animationMovementSpeed => _movementState.MovementSpeed / _movementState.MaxMovementSpeed;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();

            //_characterAttack.AttackStarted += OnAttackStarted;

            _movementState.MovementSpeedChanged += OnMovementSpeedChanged;
        }

        private void OnDisable()
        {
            //_characterAttack.AttackStarted -= OnAttackStarted;

            _inputService.Activated -= OnMove;
            _inputService.Deactivated -= OnStop;

            _movementState.MovementSpeedChanged += OnMovementSpeedChanged;
        }

        public void Init(IInputService inputService)
        {
            _inputService = inputService;

            _inputService.Activated += OnMove;
            _inputService.Deactivated += OnStop;
        }

        private void OnAttackStarted() => 
            _animator.SetTrigger(Animations.Player.Triggers.Attack);

        private void OnMove() => 
            _animator.SetBool(Animations.Player.Bools.IsMoved, true);

        private void OnStop() => 
            _animator.SetBool(Animations.Player.Bools.IsMoved, false);

        private void OnMovementSpeedChanged() => 
            _animator.SetFloat(Animations.Player.Floats.MovementAnimationSpeed, _animationMovementSpeed);
    }
}
