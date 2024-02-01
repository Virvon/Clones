using UnityEngine;
using Clones.StateMachine;
using Clones.Services;

namespace Clones.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationSwitcher : MonoBehaviour
    {
        private const float DefaultAttackAnimationSpeed = 2;

        [SerializeField] private CharacterAttack _characterAttack;

        private IInputService _inputService;
        private Animator _animator;
        private bool _isMoved;
        private float _defaultMovementSpeed;
        private Player _player;

        private float AnimationMovementSpeed => _player.StatsProvider.GetStats().MovementSpeed / _defaultMovementSpeed;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();

            _characterAttack.AttackStarted += OnAttackStarted;
        }

        private void Update() => 
            _animator.SetFloat(AnimationPath.Player.Float.MovementAnimationSpeed, AnimationMovementSpeed);

        private void OnDestroy()
        {
            _characterAttack.AttackStarted -= OnAttackStarted;

            _inputService.Activated -= OnMove;
            _inputService.Deactivated -= OnStop;
        }

        public void Init(IInputService inputService, Player player, int attackAnimationSpeedMultiplierPercent)
        {
            _inputService = inputService;
            _player = player;

            _defaultMovementSpeed = _player.StatsProvider.GetStats().MovementSpeed;
            _animator.SetFloat(AnimationPath.Player.Float.AttackAnimationSpeed, DefaultAttackAnimationSpeed * (1 + attackAnimationSpeedMultiplierPercent / 100f));

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
    }
}
