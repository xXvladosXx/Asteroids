using System;
using System.Collections.Generic;
using BonusesSystem;

namespace StatsSystem.Core
{
    public interface IModifier
    {
        IEnumerable<IBonus> FindBonus(Stat[] stats);
    }
}