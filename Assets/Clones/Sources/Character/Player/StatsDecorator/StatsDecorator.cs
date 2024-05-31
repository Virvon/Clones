namespace Clones.Character.Player
{
    public abstract class StatsDecorator : IStatsProvider
    {
        protected readonly IStatsProvider WrappedEntity;

        protected StatsDecorator(IStatsProvider wrappedEntity) =>
            WrappedEntity = wrappedEntity;

        public PlayerStats GetStats() =>
            GetStatsInternal();

        protected abstract PlayerStats GetStatsInternal();
    }
}