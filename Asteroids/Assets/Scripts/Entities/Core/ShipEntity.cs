using System;
using Combat;
using Combat.Core;
using Core;
using EnemiesZenject;
using Interaction;
using StatsSystem;
using StatsSystem.Core;
using UnityEngine;

namespace Entities.Core
{
    [RequireComponent(typeof(AttackMaker))]
    public abstract class ShipEntity : Entity, IAttackApplier, IDamageReceiver, IScoreCollector
    {
        [field: SerializeField] public ObjectPicker ObjectPicker { get; private set; }

        [SerializeField] protected AttackMaker AttackMaker;
        public Transform User => transform;
        public IScoreCollector ScoreCollector => this;

        public int Points { get; set; }

        public abstract event Action<IAttackApplier> OnDied;

        protected override void Awake()
        {
            AttackMaker.Init(ObjectPicker);

            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        public void ApplyAttack(HitData hitData, IDamageReceiver damageReceiver)
        {
            if (AttackMaker.CanMakeFire())
                AttackMaker.Fire(hitData);
        }
        
        public void ReceiveDamage(HitData hitData)
        {
            Heath.DecreaseHealth(hitData);
        }

    }
}