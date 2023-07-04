using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestKillsController : MonoBehaviour
{
    [SerializeField] private EnemiesSpawner _spawner;
    [SerializeField] private GameObject _map;
    [SerializeField] private CurrencyCounter _currencyCounter;

    private List<IDamageble> _damagebles = new List<IDamageble>();

    private void OnEnable()
    {
        _damagebles = _map.GetComponentsInChildren<IDamageble>().ToList();

        foreach (var damageble in _damagebles)
            damageble.Died += OnDied;

        _spawner.EnemyCreated += OnEnemyCreated;
    }

    private void OnDisable()
    {
        _spawner.EnemyCreated -= OnEnemyCreated;

        foreach (var damageble in _damagebles)
            damageble.Died -= OnDied;
    }

    private void OnEnemyCreated(Enemy enemy)
    {
        _damagebles.Add(enemy);

        enemy.Died += OnDied;
    }

    private void OnDied(IDamageble damageble)
    {
        if(damageble is IVisitoreble)
            _currencyCounter.OnKill((IVisitoreble)damageble);

        damageble.Died -= OnDied;
        _damagebles.Remove(damageble);
    }
}
