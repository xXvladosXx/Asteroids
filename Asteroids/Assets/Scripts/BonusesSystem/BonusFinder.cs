using System;
using System.Collections.Generic;
using System.Linq;
using StatsSystem.Core;

namespace BonusesSystem
{
    public class BonusFinder
    {
        private readonly List<IModifier> _modifiers;

        public BonusFinder(BonusHandler bonusHandler)
        {
            _modifiers = new List<IModifier> {bonusHandler};
        }
        
        public float GetBonus(Stat stat)
        {
            bool IsBonusAssignableToCharacteristics(IBonus bonus) 
                => (bonus, stat) switch
                {
                    (ProjectileLineBonus b, Stat.ProjectileLine) => true,
                    (DamageBonus b, Stat.Damage) => true,
                    (HealthRegenerationBonus b, Stat.HealthRegeneration) => true,
                    (ProjectileSpeedBonus b, Stat.ProjectileSpeed) => true,
                    _ => false
                };

            return _modifiers
                .SelectMany(x => x.FindBonus(new[] { stat }))
                .Where(IsBonusAssignableToCharacteristics)
                .Sum(x => x.Value);
        }
    }
}