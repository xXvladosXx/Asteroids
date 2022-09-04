using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StatsSystem.Core;
using UnityEngine;
using Zenject;

namespace BonusesSystem
{
    public class BonusHandler : CooldownSystem, IInitializable, ITickable, IDisposable, IModifier
    {
        public List<TimeableBonus> BonusList { get; private set; } = new List<TimeableBonus>();
        
        public event Action OnStatModified;

        public IEnumerable<IBonus> FindBonus(Stat[] stats)
        {
            IBonus CharacteristicToBonus(Stat c, float value)
                => c switch {
                    Stat.ProjectileLine => new ProjectileLineBonus(value),
                    Stat.Damage => new DamageBonus(value),
                    Stat.HealthRegeneration => new HealthRegenerationBonus(value),
                    Stat.ProjectileSpeed => new ProjectileSpeedBonus(value),
                    _ => throw new IndexOutOfRangeException()
                };

            return BonusList
                .Select(x => CharacteristicToBonus(x.Stat, x.Value));
        }

        protected override void OnTimeableRemoved(CooldownData cooldownData)
        {
            for (int i = BonusList.Count - 1; i >= 0; i--)
            {
                if (BonusList[i].Id == cooldownData.Id)
                {
                    BonusList.RemoveAt(i);
                }
            }
        }

        public void Initialize()
        {
        }
        
        public void Tick()
        {
            Update(Time.deltaTime);
        }
        
        public void AddBonus(TimeableBonus bonus)
        {
            BonusList.Add(bonus);
            PutOnCooldown(bonus);
        }

        public void RemoveBonus(TimeableBonus bonusSo)
        {
            BonusList.Add(bonusSo);
        }
        
        public void Dispose()
        {
        }
    }
}