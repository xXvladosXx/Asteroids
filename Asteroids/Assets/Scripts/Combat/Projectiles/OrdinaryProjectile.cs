using Combat.Core;
using Combat.Projectiles.Core;
using ObjectPoolers;
using UnityEngine;
using Utilities.Extensions;
using Zenject;

namespace Combat.Projectiles
{
    public class OrdinaryProjectile : Projectile, IPoolable<float, float, IMemoryPool>
    {
        private float _startTime;
        private float _speed;
        private float _lifeTime;
        private IMemoryPool _pool;

        public override void ApplyAttack(HitData hitData)
        {
            base.ApplyAttack(hitData);
            Rigidbody2D.AddForce(hitData.AttackApplier.User.up * ProjectileData.ProjectileSpeed);

            this.CallWithDelay(ReleaseProjectile, ProjectileData.MaxLifeTime);
        }

        protected override void ReleaseProjectile()
        {
            ProjectilePool.Instance.ReleasePrefab(this);
        }

        public void OnSpawned(float speed, float lifeTime, IMemoryPool pool)
        {
            _pool = pool;
            _speed = speed;
            _lifeTime = lifeTime;

            _startTime = Time.realtimeSinceStartup;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public class Factory : PlaceholderFactory<float, float, IMemoryPool>
        {
        }
    }
}