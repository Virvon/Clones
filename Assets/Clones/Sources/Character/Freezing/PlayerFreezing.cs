using UnityEngine;
using Clones.StateMachine;
using System;
using System.Collections;

public class PlayerFreezing : MonoBehaviour, IFreezingPercentChanger
{
    [SerializeField] private float _freezingSpeed;
    [SerializeField] private float _defrostSpeed;
    [SerializeField, Range(0, 100)] private int _movementSpeedFreezePercent;
    [SerializeField, Range(0, 100)] private float _damagePercent;
    [SerializeField] private float _damageCooldown;
    [SerializeField] private MovementState _movementState;
    [SerializeField] private PlayerHealth _playerHealth;

    private const int MathRoundValue = 3;

    private Coroutine _freezer;

    public float FreezingPercent { get; private set; }
    public float MovementSpeed { get; private set; }

    public event Action FreezingPercentChanged;
    public event Action MovementSpeedChanged;

    public void Freez() => 
        ChangeFreezePercent(1, _freezingSpeed);

    public void Defrost() => 
        ChangeFreezePercent(0, _defrostSpeed);

    private void ChangeFreezePercent(float targetFreezPrecent, float speed)
    {
        if (_freezer != null)
            StopCoroutine(_freezer);

        if (targetFreezPrecent < FreezingPercent)
            speed *= FreezingPercent;
        else
            speed -= speed * FreezingPercent;

        //_movementState.SetMovementSpeedChanger(this);

        _freezer = StartCoroutine(Freezer(targetFreezPrecent, speed));
    }

    private void Damage() => 
        StartCoroutine(Damager(_playerHealth));

    private IEnumerator Freezer(float targetFreezPrecent, float freezingSpeed)
    {
        float time = 0;
        float startFreezPrecent = FreezingPercent;
        targetFreezPrecent = (float)Math.Round(targetFreezPrecent, MathRoundValue);

        while (FreezingPercent != targetFreezPrecent)
        {
            time += Time.deltaTime;

            FreezingPercent = (float)Math.Round(Mathf.Lerp(startFreezPrecent, targetFreezPrecent, time / freezingSpeed), MathRoundValue);

            //MovementSpeed = (float)Math.Round(Mathf.Lerp(_movementState.MaxMovementSpeed, _movementState.MaxMovementSpeed * (_movementSpeedFreezePercent / 100f), FreezingPercent), MathRoundValue);

            FreezingPercentChanged?.Invoke();
            MovementSpeedChanged?.Invoke();



            yield return null;
        }

        if (FreezingPercent == 1)
            Damage();
    }

    private IEnumerator Damager(PlayerHealth playerHealth)
    {
        WaitForSeconds delay = new(_damageCooldown);
        float damage = playerHealth.MaxHealth * (_damagePercent / 100f);

        while (FreezingPercent == 1)
        {
            playerHealth.TakeDamage(damage);

            yield return delay;
        }
    }
}
