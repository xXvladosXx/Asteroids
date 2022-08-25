using System;
using Combat;
using Combat.Core;
using EnemiesZenject;
using Interaction;
using StatsSystem;
using StatsSystem.Core;
using UnityEngine;

namespace Entities.Core
{
    [RequireComponent(typeof(AttackMaker))]
    public abstract class ShipEntity : Entity, IAttackApplier, IDamageReceiver
    {
        [field: SerializeField] public ObjectPicker ObjectPicker { get; private set; }

        [SerializeField] protected AttackMaker AttackMaker;

        protected override void Awake()
        {
            AttackMaker.Init(ObjectPicker);

            Heath = new Heath(StatsData.GetStat(Stats.Health));
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        public void ApplyAttack(HitData hitData)
        {
            if (AttackMaker.CanMakeFire())
                AttackMaker.Fire(hitData);
        }

        public void ReceiveDamage(HitData hitData)
        {
            Heath.DecreaseHealth(hitData.Damage);
        }
    }
}