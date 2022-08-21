using System;
using StatsSystem.Core;
using UnityEngine;

namespace StatsSystem
{
    public class Heath : IStatsable
    {
        public float CurrentValue { get; private set; }
        public float MaxValue { get; }

        public event Action OnDied;
        public event Action<float, float> OnDamageReceived;

        public Heath(float value)
        {
            CurrentValue = value;
            MaxValue = value;
        }

        public void DecreaseHealth(float value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - value, 0, MaxValue);

            OnDamageReceived?.Invoke(MaxValue, CurrentValue);
            
            if (CurrentValue == 0)
            {
                OnDied?.Invoke();
            }
        }
    }
}