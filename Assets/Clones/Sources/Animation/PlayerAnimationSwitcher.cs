using UnityEngine;
using Clones.StateMachine;
using Clones.Services;

namespace Clones.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationSwitcher : MonoBehaviour
    {
        [SerializeField] private CharacterAttack _characterAttack;
        [SerializeField] private MovementState _movementState;

        private IInputService _inputService;
        private Animator _animator;
        private bool _isMoved;

        private float _animationMovementSpeed => _movementState.MovementSpeed / _movementState.MaxMovementSpeed;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();

            _characterAttack.AttackStarted += OnAttackStarted;

            _movementState.MovementSpeedChanged += OnMovementSpeedChanged;
        }

        private void OnDestroy()
        {
            _characterAttack.AttackStarted -= OnAttackStarted;

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
            _animator.SetTrigger(AnimationPath.Player.Trigger.Attack);


        private void OnMove()
        {
            if (_isMoved == false)
            {
                _animator.SetBool(AnimationPath.Player.Bool.IsMoved, true);
                _isMoved = true;
            }
        }

        private void OnStop()
        {
            if(_isMoved)
            {
                _animator.SetBool(AnimationPath.Player.Bool.IsMoved, false);
                _isMoved = false; ;
            }
        }

        private void OnMovementSpeedChanged() => 
            _animator.SetFloat(AnimationPath.Player.Float.MovementAnimationSpeed, _animationMovementSpeed);
    }
}
