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
        [field: SerializeField] public Heath Heath { get; private set; }
        [field: SerializeField] public StatsData StatsData { get; private set; }
        
        [Inject]
        public void Construct(Heath heath)
        {
            Heath = heath;
        }
        
        public abstract void Die();
        public IDamagable Damagable { get; set; }
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