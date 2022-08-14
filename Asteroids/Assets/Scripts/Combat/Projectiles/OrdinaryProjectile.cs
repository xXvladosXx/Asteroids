using Combat.Projectiles.Core;
using Data.Bullet;
using UnityEngine;

namespace Combat.Projectiles
{
    public class OrdinaryProjectile : Projectile
    {
        public override void Fire(Vector2 direction)
        {
            base.Fire(direction);
            Rigidbody2D.AddForce(direction * ProjectileData.ProjectileSpeed);
            
            Destroy(gameObject, ProjectileData.MaxLifeTime);
        }
        
        protected override void OnCollisionEnter2D(Collision2D col)
        {
            base.OnCollisionEnter2D(col);
            Destroy(gameObject);
        }
    }
}