using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public IStatsProvider StatsProvider { get; private set; }

    public event Action StatsProviderChanged;

    public void Init(float movementSpeed, float attackCooldown)
    {
        Decorate(new DefaultStats(movementSpeed, attackCooldown));
    }

    public void Decorate(IStatsProvider statsProvider)
    {
        StatsProvider = statsProvider;

        StatsProviderChanged?.Invoke();
    }
}
