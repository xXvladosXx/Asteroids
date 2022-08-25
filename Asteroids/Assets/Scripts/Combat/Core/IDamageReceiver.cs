using StatsSystem;

namespace Combat.Core
{
    public interface IDamageReceiver
    {
        void ReceiveDamage(HitData hitData);
    }
}