using System.Collections;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _delay;
    [SerializeField] private int _count;

    [SerializeField] private TargetArea _targetArea;
    [SerializeField] private Player _target;

    private void Start() => StartCoroutine(Spawner());

    private void CreateWave()
    {
        for(var i = 0; i < _count; i++)
        {
            var enemy = Instantiate(_enemyPrefab, GetRandomPositionOutSideScreen(), Quaternion.identity, transform);
            enemy.Init(_target, _targetArea);
        }
    }

    private Vector3 GetRandomPositionOutSideScreen()
    {
        Vector2 point = Random.insideUnitCircle.normalized * 20;

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
