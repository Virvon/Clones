using Clones.Services;
using System;
using System.Collections;
using UnityEngine;

public class Freezing : StatsDecorator
{
    private const float FreezingSpeed = 15;
    private const float DefrostSpeed = 5;
    private const int MovementSpeedFreezePercent = 35;
    private const int AttackCooldownFreezePercent = 160;
    private const float DamagePercent = 15;
    private const float DamageCooldown = 1;

    private readonly float _defaultMovementSpeed;
    private readonly float _defaultAttackCooldown;
    private readonly PlayerHealth _playerHealth;

    private ICoroutineRunner _coroutineRunner;
    private Coroutine _freezer;
    private float CurrentMovementSpeed;
    private float CurrentAttackCooldown;

    public event Action<float> FreezPercentChanged;

    public float CurrentFreezingPercent { get; private set; }

    public Freezing(IStatsProvider wrappedEntity, PlayerHealth playerHealth) : base(wrappedEntity)
    {
        _playerHealth = playerHealth;

        _defaultMovementSpeed = wrappedEntity.GetStats().MovementSpeed;
        _defaultAttackCooldown = wrappedEntity.GetStats().AttackCooldown;
    }

    public void SetCoroutineRunner(ICoroutineRunner coroutineRunner) => 
        _coroutineRunner = coroutineRunner;

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

        while (CurrentFreezingPercent >= 100)
        {
            playerHealth.TakeDamage(damage);

            yield return delay;
        }
    }
}