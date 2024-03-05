using Clones.StateMachine;
using System;
using UnityEngine;

namespace Clones.SFX
{
    public class MovementSound : MonoBehaviour
    {
        private const float DefaultPitch = 1.5f;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private MovementState _movementState;

        private Player _player;
        private float _defaultSpeed;

        private float Pitch => _player.StatsProvider.GetStats().MovementSpeed / _defaultSpeed * DefaultPitch;

        public void Init(Player player)
        {
            _player = player;

            _defaultSpeed = _player.StatsProvider.GetStats().MovementSpeed;

            _movementState.Started += OnMovementStarted;
            _movementState.Stopped += OnStopped;
        }

        private void Update() => 
            _audioSource.pitch = Pitch;

        private void OnDestroy()
        {
            _movementState.Started -= OnMovementStarted;
            _movementState.Stopped -= OnStopped;
        }

        private void OnMovementStarted() => 
            _audioSource.Play();

        private void OnStopped() => 
            _audioSource.Stop();
    }
}
