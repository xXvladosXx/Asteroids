using Combat;
using Combat.Core;
using Interaction;
using StatsSystem.Core;

namespace BonusesSystem
{
    public interface IBonusUser
    {
        BonusFinder BonusFinder { get; }
    }
}