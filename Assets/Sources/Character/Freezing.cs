using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Freezing : MonoBehaviour
{
    [SerializeField] private float _freezingMovementSpeed;
    [SerializeField] private float _freezingAttackSpeed;

    public float FreezPrecent { get; private set; }

    private Coroutine _coroutine;
    private MovementStats _playerMovementStats;

    private float _currentMovementSpeed;
    private float _currentAttackSpeed;

    private float _movementSpeed;
    private float _attackSpeed;

    public event Action FreePrecentChanged;

    private void Start()
    {
        _playerMovementStats = GetComponent<Player>().MovementStats;

        _movementSpeed = _playerMovementStats.MovementSpeed;
        _attackSpeed = _playerMovementStats.AttakcSpeed;
    }

    public void Freez(float targetFreezPrecent, float speed)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (targetFreezPrecent < FreezPrecent)
            speed = speed * FreezPrecent;
        else
            speed = speed - speed * FreezPrecent;

        _coroutine = StartCoroutine(FreezController(targetFreezPrecent, speed));
    }

    private IEnumerator FreezController(float targetFreezPrecent, float freezingSpeed)
    {
        float time = 0;
        float startFreezPrecent = FreezPrecent;

        while(FreezPrecent != targetFreezPrecent)
        {
            time += Time.deltaTime;

            FreezPrecent = Mathf.Lerp(startFreezPrecent, targetFreezPrecent, time / freezingSpeed);
            _currentMovementSpeed = Mathf.Lerp(_movementSpeed, _freezingMovementSpeed, FreezPrecent);
            _currentAttackSpeed = Mathf.Lerp(_attackSpeed, _freezingAttackSpeed, FreezPrecent);

            _playerMovementStats.Freez(_currentMovementSpeed, _currentAttackSpeed);
            FreePrecentChanged?.Invoke();

            yield return null;
        }
    }
}
