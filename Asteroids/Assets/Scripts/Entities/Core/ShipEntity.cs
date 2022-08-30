using System;
using Combat;
using Combat.Core;
using Core;
using Interaction;
using StatsSystem;
using StatsSystem.Core;
using UnityEngine;

namespace Entities.Core
{
    [RequireComponent(typeof(AttackHandler))]
    public abstract class ShipEntity : Entity, IAttackApplier, IDamageReceiver, IScoreCollector
    {
        [field: SerializeField] public ObjectPicker ObjectPicker { get; private set; }
        [field: SerializeField] public AttackHandler AttackHandler { get; private set; }
        public Transform User => transform;
        public IScoreCollector ScoreCollector => this;

        public int Points { get; set; }

        public abstract event Action<IAttackApplier> OnDied;

        protected override void Awake()
        {
            AttackHandler.Init();

            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        public void ApplyAttack(HitData hitData, IDamageReceiver damageReceiver)
        {
            if (AttackHandler.CanMakeFire())
                AttackHandler.Fire(hitData, transform.position);
        }
        
        public virtual void ReceiveDamage(HitData hitData)
        {
            Heath.DecreaseHealth(hitData);
        }

    }
}