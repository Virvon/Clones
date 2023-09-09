using Clones.StateMachine;
using System;
using System.Collections;
using UnityEngine;

namespace Clones.Biomes
{
    public class IceCrystals : Biome, IMovementSpeedChanger
    {
        [SerializeField] private float _freezingSpeed;
        [SerializeField] private float _defrostSpeed;
        [SerializeField, Range(0, 100)] private int _movementSpeedFreezePercent;

        private const int MathRoundValue = 3;

        private Coroutine _freezer;
        private MovementState _movementState;

        public float FreezingPercent { get; private set; }
        public float MovementSpeed { get; private set; }

        public event Action FreezingPercentChanged;

        private void OnEnable()
        {
            PlayerEntered += OnPlayerEntered;
            PlayerExited += OnPlayerExited;
        }

        private void OnDisable()
        {
            PlayerEntered -= OnPlayerEntered;
            PlayerExited -= OnPlayerExited;
        }

        private void OnPlayerEntered(Biome biome)
        {
            _movementState = Player.GetComponent<MovementState>();
            _movementState.GetMovementSpeedChanger(this);

            ChangeFreezePercent(1, _freezingSpeed);
        }

        private void OnPlayerExited() =>
            ChangeFreezePercent(0, _defrostSpeed);

        private void ChangeFreezePercent(float targetFreezPrecent, float speed)
        {
            if (_freezer != null)
                StopCoroutine(_freezer);

            if (targetFreezPrecent < FreezingPercent)
                speed *= FreezingPercent;
            else
                speed -= speed * FreezingPercent;

            _freezer = StartCoroutine(Freezer(targetFreezPrecent, speed));
        }

        private IEnumerator Freezer(float targetFreezPrecent, float freezingSpeed)
        {
            float time = 0;
            float startFreezPrecent = FreezingPercent;
            targetFreezPrecent = (float)Math.Round(targetFreezPrecent, MathRoundValue);

            while (FreezingPercent != targetFreezPrecent)
            {
                time += Time.deltaTime;

                FreezingPercent = (float)Math.Round(Mathf.Lerp(startFreezPrecent, targetFreezPrecent, time / freezingSpeed), MathRoundValue);

                MovementSpeed = (float)Math.Round(Mathf.Lerp(_movementState.MaxMovementSpeed, _movementState.MaxMovementSpeed * (_movementSpeedFreezePercent / 100f), FreezingPercent), MathRoundValue);

                FreezingPercentChanged?.Invoke();

                yield return null;
            }
        }
    }
}
