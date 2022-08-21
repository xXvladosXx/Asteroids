using System;
using Combat.Core;
using Combat.Projectiles.Modifiers;
using Data.Projectile;
using Entities;
using UnityEngine;

namespace Combat.Projectiles.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour, IHitApplier
    {
        [field: SerializeField] public ProjectileData ProjectileData { get; private set; }
        [field: SerializeField] public ProjectileModifier[] ProjectileModifiersOnStart { get; private set; }
        [field: SerializeField] public ProjectileModifier[] ProjectileModifiersOnDestroy { get; private set; }
        
        protected Rigidbody2D Rigidbody2D;
        private HitData _hitData;
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public virtual void ApplyHit(HitData hitData)
        {
            _hitData = hitData;
        }

        protected virtual void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.TryGetComponent(out IDamagable hurtable))
            {
                hurtable.ReceiveDamage(_hitData);
            }
        }

        
    }
}
