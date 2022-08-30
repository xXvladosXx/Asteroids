using System;
using Combat.Core;
using Combat.Projectiles.Core;
using UnityEngine;
using Utilities.Extensions;
using Zenject;

namespace Combat.Projectiles
{
    public class OrdinaryProjectile : Projectile, IPoolable<IMemoryPool>
    {
        public override void ApplyAttack(HitData hitData, ProjectileModifiersData projectileModifiersData)
        {
            base.ApplyAttack(hitData, projectileModifiersData);
            Rigidbody2D.AddForce(hitData.AttackApplier.User.up *
                                 (ProjectileData.ProjectileSpeed + projectileModifiersData.AdditionalSpeed));

            this.CallWithDelay(ReleaseProjectile, ProjectileData.MaxLifeTime);
        }

        protected override void ReleaseProjectile()
        {
            Pool?.Despawn(this);
        }

        public void OnSpawned(IMemoryPool pool)
        {
            Pool = pool;
        }

        public void OnDespawned()
        {
            Pool = null;
        }

        public class Factory : PlaceholderFactory<OrdinaryProjectile>
        {
        }
    }
}