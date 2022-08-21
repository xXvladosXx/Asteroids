namespace StatsSystem.Core
{
    public interface IStatsable
    {
        float CurrentValue { get; }
        float MaxValue { get; }
    }
}