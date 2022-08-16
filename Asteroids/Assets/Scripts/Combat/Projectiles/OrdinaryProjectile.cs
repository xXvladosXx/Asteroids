using Combat.Projectiles.Core;
using Data.Bullet;
using UnityEngine;
using Utilities.Extensions;

namespace Combat.Projectiles
{
    public class OrdinaryProjectile : Projectile
    {
        public override void Fire(Vector2 direction)
        {
            base.Fire(direction);
            Rigidbody2D.AddForce(direction * ProjectileData.ProjectileSpeed);
            
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