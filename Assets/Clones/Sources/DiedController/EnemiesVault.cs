using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DieReporter))]
public class EnemiesVault : MonoBehaviour
{
    [SerializeField] private EnemiesSpawner _spawner;

    private DieReporter _dieReporter;

    private void OnEnable()
    {
        _spawner.EnemyCreated += OnEnemyCreated;
        _dieReporter = GetComponent<DieReporter>();
    }

    private void OnDisable() => _spawner.EnemyCreated -= OnEnemyCreated;

    private void OnEnemyCreated(Enemy enemy) => _dieReporter.TakeIDamagebles(new List<IDamageable> { enemy });
}
