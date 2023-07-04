using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _delay;

    [SerializeField] private TargetArea _targetArea;
    [SerializeField] private Player _target;

    private int _wave;
    private EnemyWeightCounter _weightCounter;

    public event Action<Enemy> EnemyCreated;

    private void Start() => StartCoroutine(Spawner());

    private void CreateWave()
    {
        _wave++;
        _weightCounter = new EnemyWeightCounter(_wave);

        Stats enemyStats;

        while(_weightCounter.TryGetEnemyStats(out enemyStats))
        {
            var enemy = Instantiate(_enemyPrefab, GetRandomPositionOutSideScreen(), Quaternion.identity, transform);
            enemy.Init(_target, _targetArea, enemyStats);

            EnemyCreated?.Invoke(enemy);
        }
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
            CreateWave();
            yield return new WaitForSeconds(_delay);
        }
    }
}
