using System;
using System.Collections.Generic;
using BonusesSystem;

namespace StatsSystem.Core
{
    public interface IModifier
    {
        public event Action OnStatModified;
        IEnumerable<IBonus> FindBonus(Stat[] stats);
    }
}