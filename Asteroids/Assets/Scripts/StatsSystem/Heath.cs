using System;
using Combat.Core;
using StatsSystem.Core;
using UnityEngine;

namespace StatsSystem
{
    public class Heath : IStatsable
    {
        public float CurrentValue { get; private set; }
        public float MaxValue { get; }

        public event Action<IAttackApplier> OnDied;
        public event Action<float, float> OnHealthChanged;

        public Heath(float value)
        {
            CurrentValue = value;
            MaxValue = value;
        }
        public void IncreaseHealth(float value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue + value, 0, MaxValue);
            
            OnHealthChanged?.Invoke(MaxValue, CurrentValue);
        }
        
        public void DecreaseHealth(HitData hitData)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - hitData.Damage, 0, MaxValue);

            OnHealthChanged?.Invoke(MaxValue, CurrentValue);
            
            if (CurrentValue == 0)
            {
                OnDied?.Invoke(hitData.AttackApplier);
            }
        }
    }
}