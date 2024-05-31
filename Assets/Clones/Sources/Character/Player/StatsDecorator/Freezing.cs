using Clones.Services;
using System;
using System.Collections;
using UnityEngine;

namespace Clones.Character.Player
{
    public class Freezing : StatsDecorator
    {
        private const float FreezingSpeed = 40;
        private const float DefrostSpeed = 5;
        private const int MovementSpeedFreezePercent = 25;
        private const int AttackCooldownFreezePercent = 150;
        private const float DamagePercent = 10;
        private const float DamageCooldown = 1;

        private readonly float _defaultMovementSpeed;
        private readonly float _defaultAttackCooldown;
        private readonly PlayerHealth _playerHealth;
        private readonly ICoroutineRunner _coroutineRunner;

        private Coroutine _freezer;
        private float CurrentMovementSpeed;
        private float CurrentAttackCooldown;

        public Freezing(IStatsProvider wrappedEntity, PlayerHealth playerHealth, ICoroutineRunner coroutineRunner) : base(wrappedEntity)
        {
            _playerHealth = playerHealth;
            _coroutineRunner = coroutineRunner;

            _defaultMovementSpeed = wrappedEntity.GetStats().MovementSpeed;
            _defaultAttackCooldown = wrappedEntity.GetStats().AttackCooldown;
        }

        public event Action<float> FreezPercentChanged;

        public float CurrentFreezingPercent { get; private set; }

        public void Freez() =>
            ChangeFreezePercent(100, FreezingSpeed);

        public void Defrost() =>
            ChangeFreezePercent(0, DefrostSpeed);

        protected override PlayerStats GetStatsInternal()
        {
            return new PlayerStats()
            {
                MovementSpeed = CurrentMovementSpeed,
                AttackCooldown = CurrentAttackCooldown
            };
        }

        private void ChangeFreezePercent(int targetFreezPrecent, float speed)
        {
            if (_freezer != null)
                _coroutineRunner.StopCoroutine(_freezer);

            if (targetFreezPrecent < CurrentFreezingPercent)
                speed *= (CurrentFreezingPercent / 100f);
            else
                speed -= speed * (CurrentFreezingPercent / 100f);

            _freezer = _coroutineRunner.StartCoroutine(Freezer(targetFreezPrecent, speed));
        }

        private void Reset(IDamageable obj)
        {
            _playerHealth.Died -= Reset;

            CurrentFreezingPercent = 0;
            FreezPercentChanged?.Invoke(CurrentFreezingPercent);
        }

        private IEnumerator Freezer(int targetFreezPrecent, float freezingSpeed)
        {
            float time = 0;
            float startFreezPrecent = CurrentFreezingPercent;

            while (CurrentFreezingPercent != targetFreezPrecent)
            {
                time += Time.deltaTime;

                CurrentFreezingPercent = Mathf.Lerp(startFreezPrecent, targetFreezPrecent, time / freezingSpeed);
                CurrentMovementSpeed = Mathf.Lerp(_defaultMovementSpeed, _defaultMovementSpeed * (MovementSpeedFreezePercent / 100f), CurrentFreezingPercent / 100f);
                CurrentAttackCooldown = Mathf.Lerp(_defaultAttackCooldown, _defaultAttackCooldown * (AttackCooldownFreezePercent / 100f), CurrentFreezingPercent / 100f);

                FreezPercentChanged?.Invoke(CurrentFreezingPercent);

                yield return null;
            }

            if (CurrentFreezingPercent >= 100)
                _coroutineRunner.StartCoroutine(Damager(_playerHealth));
        }

        private IEnumerator Damager(PlayerHealth playerHealth)
        {
            WaitForSeconds delay = new(DamageCooldown);
            float damage = playerHealth.MaxHealth * (DamagePercent / 100f);

            playerHealth.Died += Reset;

            while (CurrentFreezingPercent >= 100 && playerHealth.Health > 0)
            {
                playerHealth.TakeDamage(damage);

                yield return delay;
            }
        }
    }
}