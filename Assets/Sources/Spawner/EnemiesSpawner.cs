using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using Clones.Data;
using Clones.Progression;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private SpawnerData _spawnerData;
    [SerializeField] private float _delay;

    [SerializeField] private PlayerArea _targetArea;
    [SerializeField] private Player _target;

    private WeightProgression _weightProgression = new WeightProgression();
    private StatsProgression _statsProgression = new StatsProgression();

    private float _maxWeight;
    private float _currentWeight;

    private int _currentWave;

    public event Action<Enemy> EnemyCreated;

    private void Start() => StartCoroutine(Spawner());

    private void CreateWave()
    {
        Stats stats = _statsProgression.GetStats(_currentWave, _enemyData.GetStats());
        float enemyWeight = GetEnemyWeight(stats);

        _maxWeight = _weightProgression.GetMaxWeight(_currentWave, _spawnerData.BaseTotalWeight);
        _currentWeight = 0;

        //Debug.Log("max weight " + _maxWeight + " enemy weight " + enemyWeight);

        while (_currentWeight < _maxWeight)
        {
            var enemy = Instantiate(_enemyData.EnemyPrefab, GetRandomPositionOutSideScreen(), Quaternion.identity, transform);
            enemy.Init(_target, _targetArea, stats);

            EnemyCreated?.Invoke(enemy);

            _currentWeight += enemyWeight;
        }
    }

    private float GetEnemyWeight(Stats stats)
    {
        return (1 / stats.AttackSpeed) * stats.Damage + (stats.Health / 3);
    }

    private Vector3 GetRandomPositionOutSideScreen()
    {
        Vector2 point = Random.insideUnitCircle.normalized * 20 + new Vector2(_target.transform.position.x, _target.transform.position.z);

        return new Vector3(point.x, 1, point.y);
    }

    private IEnumerator Spawner()
    {
        bool isFinish = false;

        while(isFinish == false)
        {
            _currentWave++;
            CreateWave();
            yield return new WaitForSeconds(_delay);
        }
    }
}
