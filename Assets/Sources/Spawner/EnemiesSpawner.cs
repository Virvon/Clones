using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using Clones.Data;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private SpawnerData _spawnerData;
    [SerializeField] private float _delay;

    [SerializeField] private PlayerArea _targetArea;
    [SerializeField] private Player _target;

    public event Action<Enemy> EnemyCreated;

    private void Start() => StartCoroutine(Spawner());

    private void CreateWave()
    {
        for(var i = 0; i < 4; i++)
        {
            var enemy = Instantiate(_enemyData.EnemyPrefab, GetRandomPositionOutSideScreen(), Quaternion.identity, transform);
            enemy.Init(_target, _targetArea, _enemyData.GetStats());

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
