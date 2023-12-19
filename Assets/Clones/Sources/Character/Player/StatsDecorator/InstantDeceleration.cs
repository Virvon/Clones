public class InstantDeceleration : StatsDecorator
{
    private readonly float _movementSpeedPercent;

    public InstantDeceleration(IStatsProvider wrappedEntity, float movementSpeedPercent) : base(wrappedEntity)
    {
        _movementSpeedPercent = movementSpeedPercent;
    }

    protected override PlayerStats GetStatsInternal()
    {
        return new PlayerStats()
        {
            MovementSpeed = WrappedEntity.GetStats().MovementSpeed * _movementSpeedPercent,
            AttackSpeed = WrappedEntity.GetStats().AttackSpeed
        };
    }
}