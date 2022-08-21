using StatsSystem.Core;

namespace StatsSystem
{
    public class Damage : IStatsable
    {
        public float CurrentValue { get; private set; }
        public float MaxValue { get; }
    }
}