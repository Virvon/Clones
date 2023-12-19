public class DefaultStats : IStatsProvider
{
    private readonly float _movementSpeed;
    private readonly float _attackSpeed;

    public DefaultStats(float movementSpeed, float attackSpeed)
    {
        _movementSpeed = movementSpeed;
        _attackSpeed = attackSpeed;
    }

    public PlayerStats GetStats()
    {
        return new PlayerStats()
        {
            MovementSpeed = _movementSpeed,
            AttackCooldown = _attackSpeed
        };
    }
}
