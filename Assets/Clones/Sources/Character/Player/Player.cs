using Clones.Services;
using UnityEngine;

public class Player : MonoBehaviour, ICoroutineRunner
{
    private DefaultStats _defaultStats;

    public IStatsProvider StatsProvider { get; private set; }

    public void Init(float movementSpeed, float attackCooldown)
    {
        _defaultStats = new DefaultStats(movementSpeed, attackCooldown);
        Decorate(_defaultStats);
    }

    public void Decorate(IStatsProvider statsProvider) => 
        StatsProvider = statsProvider;

    public void OnDestroy() =>
        Decorate(_defaultStats);
}