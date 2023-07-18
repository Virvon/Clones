using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private Enemy _characterPrefab;
    [SerializeField] private TargetArea _targetArea;
    [SerializeField] private Clone _target;

    private Enemy current;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        current = Instantiate(_characterPrefab);
        current.Init(_target, _targetArea);
        current.Died += OnDied;
    }

    private void OnDied()
    {
        current.Died -= OnDied;
        Spawn();
    }
}
