using UnityEngine;

public class HealthBarsSpawner : MonoBehaviour
{
    [SerializeField] private EnemyHealthbar _healthbarPrefab;
    //[SerializeField] private EnemiesSpawnerOld _emiesSpawner;
    [SerializeField] private Vector3 _healthbarOffset;

    //private void OnEnable() => _emiesSpawner.EnemyCreated += OnEnemyCreated;

    //private void OnDisable() => _emiesSpawner.EnemyCreated -= OnEnemyCreated;

    private void OnEnemyCreated(Enemy enemy)
    {
        EnemyHealthbar healthbar = Instantiate(_healthbarPrefab, enemy.transform.position + _healthbarOffset, Quaternion.identity, enemy.transform);
        //healthbar.Init(enemy);
    }
}
