using Combat.Core;
using Combat.Projectiles.Core;
using ObjectPoolers;
using UnityEngine;
using Utilities.Extensions;
using Zenject;

namespace Combat.Projectiles
{
    public class OrdinaryProjectile : Projectile
    {
        public override void ApplyAttack(HitData hitData)
        {
            base.ApplyAttack(hitData);
            Rigidbody2D.AddForce(hitData.DamageApplier.up * ProjectileData.ProjectileSpeed);
            
            this.CallWithDelay(ReleaseProjectile, ProjectileData.MaxLifeTime);
        }
        
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            base.OnTriggerEnter2D(col);
        }
        
        protected override void ReleaseProjectile()
        {
            ProjectilePool.Instance.ReleasePrefab(this);
        }
    }
}