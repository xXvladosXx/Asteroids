using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BonusesSystem
{
    public class CooldownSystem
    {
        private readonly List<CooldownData> _cooldownDatas = new List<CooldownData>();

        protected void Update(float time)
        {
            ProcessCooldowns(time);
        }

        private void ProcessCooldowns(float time)
        {
            for (int i = _cooldownDatas.Count - 1; i >= 0; i--)
            {
                if (_cooldownDatas[i].DecrementCooldown(time))
                {
                    OnTimeableRemoved(_cooldownDatas[i]);
                    RemoveFromCooldown(i);
                }
            }
        }

        

        protected virtual void OnTimeableRemoved(CooldownData cooldownData)
        {
            
        }

        protected void PutOnCooldown(ITimeable timeable)
        {
            _cooldownDatas.Add(new CooldownData(timeable));
        }
        
        private void RemoveFromCooldown(int i)
        {
            _cooldownDatas.RemoveAt(i);
        }

        public bool IsOnCooldown(int id) => _cooldownDatas.Any(cooldownData => cooldownData.Id == id);

        public float GetRemainingDuration(int id) => (from cooldownData 
            in _cooldownDatas where cooldownData.Id == id select cooldownData.RemainingTime).FirstOrDefault();
    }

    public class CooldownData
    {
        public int Id { get; }
        public float RemainingTime { get; private set; }
        
        public CooldownData(ITimeable timeable)
        {
            Id = timeable.Id;
            RemainingTime = timeable.Duration;
        }

        public bool DecrementCooldown(float deltaTime)
        {
            RemainingTime = Mathf.Max(RemainingTime - deltaTime, 0f);

            return RemainingTime == 0;
        }
    }
}