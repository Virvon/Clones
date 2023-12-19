public abstract class StatsDecorator : IStatsProvider
{
    protected readonly IStatsProvider WrappedEntity;

    protected StatsDecorator(IStatsProvider wrappedEntity)
    {
        WrappedEntity = wrappedEntity;
    }

    public PlayerStats GetStats()
    {
        return WrappedEntity.GetStats();
    }

    protected abstract PlayerStats GetStatsInternal();
}
