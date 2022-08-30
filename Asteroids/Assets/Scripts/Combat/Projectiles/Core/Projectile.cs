using System;
using Combat.Core;
using Combat.Projectiles.Modifiers;
using Core;
using Data.Projectile;
using Entities;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Combat.Projectiles.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour
    {
        [field: SerializeField] public ProjectileData ProjectileData { get; private set; }
        [field: SerializeField] public ProjectileModifier[] ProjectileModifiersOnStart { get; private set; }
        [field: SerializeField] public ProjectileModifier[] ProjectileModifiersOnDestroy { get; private set; }
        
        protected Rigidbody2D Rigidbody2D;
        private HitData _hitData;
        protected IMemoryPool Pool;

        public Transform User => _hitData.AttackApplier.User;
     
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public virtual void ApplyAttack(HitData hitData, ProjectileModifiersData projectileModifiersData)
        {
            _hitData = hitData;
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IDamageReceiver hurtable))
            {
                if(_hitData.AttackApplier == null) return;
                if(_hitData.AttackApplier.User == null) return;
                if(col.transform == User) return;
                if(col.gameObject.layer == User.gameObject.layer) return;
                
                hurtable.ReceiveDamage(_hitData);
                ReleaseProjectile();
                
                Pool?.Despawn(this);
            }
        }

        protected abstract void ReleaseProjectile();
    }
}
