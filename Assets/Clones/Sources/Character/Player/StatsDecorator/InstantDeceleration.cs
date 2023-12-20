public class InstantDeceleration : StatsDecorator
{
    private readonly int _movementSpeedPercent;

    public InstantDeceleration(IStatsProvider wrappedEntity, int movementSpeedPercent) : base(wrappedEntity)
    {
        _movementSpeedPercent = movementSpeedPercent;
    }

    protected override PlayerStats GetStatsInternal()
    {
        return new PlayerStats()
        {
            
            MovementSpeed = WrappedEntity.GetStats().MovementSpeed * (_movementSpeedPercent / 100f),
            AttackCooldown = WrappedEntity.GetStats().AttackCooldown
        };
    }
}
