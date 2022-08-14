using System;
using Combat.Projectiles.Modifiers;
using Data.Bullet;
using Entities;
using UnityEngine;

namespace Combat.Projectiles.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour
    {
        [field: SerializeField] public ProjectileData ProjectileData { get; private set; }
        [field: SerializeField] public ProjectileModifier[] ProjectileModifiersOnStart { get; private set; }
        [field: SerializeField] public ProjectileModifier[] ProjectileModifiersOnDestroy { get; private set; }
        
        protected Rigidbody2D Rigidbody2D;

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public virtual void Fire(Vector2 direction)
        {
            var modifierData = new ModifierData()
            {
                Transform = transform
            };
            
            foreach (var projectileModifier in ProjectileModifiersOnStart)
            {
                projectileModifier.ApplyModifier(modifierData);
            }   
        }

        protected virtual void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.TryGetComponent(out AsteroidEntity asteroidEntity))
            {
                asteroidEntity.Die();
            }
        }
    }
}
