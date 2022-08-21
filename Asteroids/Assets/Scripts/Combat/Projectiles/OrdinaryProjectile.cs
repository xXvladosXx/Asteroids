using Combat.Core;
using Combat.Projectiles.Core;
using ObjectPoolers;
using UnityEngine;
using Utilities.Extensions;

namespace Combat.Projectiles
{
    public class OrdinaryProjectile : Projectile
    {
        public override void ApplyHit(HitData hitData)
        {
            base.ApplyHit(hitData);
            Rigidbody2D.AddForce(hitData.Transform.up * ProjectileData.ProjectileSpeed);
            
            this.CallWithDelay(ReleaseProjectile, ProjectileData.MaxLifeTime);
        }
        
        protected override void OnCollisionEnter2D(Collision2D col)
        {
            base.OnCollisionEnter2D(col);
            ReleaseProjectile();
        }
        
        private void ReleaseProjectile()
        {
            ProjectilePool.Instance.ReleasePrefab(this);
        }
    }
}