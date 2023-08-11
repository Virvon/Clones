using System;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(Freezing))]
public class FreezingMovement : MonoBehaviour 
{
    [SerializeField] private float _freezingMovementSpeedPercent;
    [SerializeField] private float _freezingAttackCooldownPercent;

    private MovementStats _playerMovementStats;
    private Freezing _freezing;

    private float _movementSpeed;
    private float _attackCooldown;

    private float _freezingMovementSpeed;
    private float _freezingAttackSpeed;

    private void OnEnable()
    {   
        _freezing = GetComponent<Freezing>();

        _freezing.FreezingPrecentChanged += OnFreezPrecentChanged;
    }

    private void Start()
    {
        _playerMovementStats = GetComponent<Player>().MovementStats;

        _movementSpeed = _playerMovementStats.MovementSpeed;
        _attackCooldown = _playerMovementStats.AttakcCooldown;

        _freezingMovementSpeed = _playerMovementStats.MovementSpeed * (_freezingMovementSpeedPercent / 100);
        _freezingAttackSpeed = _playerMovementStats.AttakcCooldown * (_freezingAttackCooldownPercent / 100);
    }

    private void OnDisable() => _freezing.FreezingPrecentChanged -= OnFreezPrecentChanged;

    private void OnFreezPrecentChanged()
    {
        var currentMovementSpeed = Mathf.Lerp(_movementSpeed, _freezingMovementSpeed, _freezing.FreezPrecent);
        var currentAttackCooldown = Mathf.Lerp(_attackCooldown, _freezingAttackSpeed, _freezing.FreezPrecent);

        currentMovementSpeed = (float)Math.Round((double)currentMovementSpeed, 2);
        currentAttackCooldown = (float)Math.Round((double)currentAttackCooldown, 2);

        _playerMovementStats.Freez(currentMovementSpeed, currentAttackCooldown);
    }
}
