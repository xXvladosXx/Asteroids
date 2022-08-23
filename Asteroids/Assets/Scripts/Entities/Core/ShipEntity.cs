using Combat;
using Combat.Core;
using Interaction;
using UnityEngine;

namespace Entities.Core
{
    [RequireComponent(typeof(AttackMaker))]
    public abstract class ShipEntity : Entity, IDamagable, IAttackApplier
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public ObjectPicker ObjectPicker { get; private set; }

        [SerializeField] protected AttackMaker AttackMaker;

        protected override void Awake()
        {
            AttackMaker.Init(ObjectPicker);
        }

        public abstract void ApplyAttack(HitData hitData);

        public void ReceiveDamage(HitData hitData)
        {
            Heath.DecreaseHealth(hitData.Damage);
        }
    }
}