using System;
using Combat.Core;
using StatsSystem;
using StatsSystem.Core;
using UnityEngine;
using Zenject;

namespace Entities.Core
{
    public abstract class Entity : MonoBehaviour, IInitializable, IDisposable, IHurtbox
    {
        [field: SerializeField] public Heath Heath { get; protected set; }
        [field: SerializeField] public StatsData StatsData { get; private set; }
        
        public virtual void Construct()
        {
            Heath = new Heath(StatsData.GetStat(Stats.Health));
        }
        
        public abstract void Die(IAttackApplier attackApplier);
        protected virtual void Awake() { }
        public void Initialize()
        {
            Heath.OnDied += Die;
        }

        public void Dispose()
        {
            Heath.OnDied -= Die;
        }
    }
}