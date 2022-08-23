using System;
using AsteroidZenject;
using Combat.Core;
using Combat.Projectiles.Modifiers;
using Data.Projectile;
using Entities;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Combat.Projectiles.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour, IAttackApplier, IPoolable<IMemoryPool>
    {
        [field: SerializeField] public ProjectileData ProjectileData { get; private set; }
        [field: SerializeField] public ProjectileModifier[] ProjectileModifiersOnStart { get; private set; }
        [field: SerializeField] public ProjectileModifier[] ProjectileModifiersOnDestroy { get; private set; }
        
        protected Rigidbody2D Rigidbody2D;
        private HitData _hitData;
        private IMemoryPool _pool;

        private float _startTime;
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Time.deltaTime - _startTime > ProjectileData.MaxLifeTime)
            {
                _pool.Despawn(this);
            }
        }

        public virtual void ApplyAttack(HitData hitData)
        {
            _hitData = hitData;
        }
        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;

            _startTime = Time.deltaTime;
        }
        protected virtual void OnCollisionEnter2D(Collision2D col)
        {
            /*if (col.transform.TryGetComponent(out IDamagable hurtable))
            {
                hurtable.ReceiveDamage(_hitData);
            }*/

            if (col.collider.TryGetComponent(out AsteroidFacade asteroidFacade))
            {
                asteroidFacade.Die();
            }
        }
        
       
    }
}
