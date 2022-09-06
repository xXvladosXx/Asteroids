using System;
using Combat.Core;
using Entities;
using Entities.Core;
using Score;
using StatsSystem.Core;
using UnityEngine;
using Utilities.Extensions;
using Zenject;

namespace AsteroidsZenject.AsteroidZenject
{
    public class AsteroidFacade : MonoBehaviour, IPoolable<float, Vector3, IMemoryPool>,
        IDisposable, IDamageReceiver, IEnemy, IAttackApplier
    {
        public AsteroidEntity AsteroidEntity { get; set; }

        private IMemoryPool _pool;
        private AsteroidDeathHandler _asteroidDeathHandler;
        
        private float _lifeTime;
        private float _enemySpawnTime;
        
        public IScoreCollector ScoreCollector { get; set; }
        public PlayerEntity PlayerEntity { get; set; }

        public Transform User => transform;
        public event Action<AsteroidFacade> OnEntitySpawned;
        public event Action<AsteroidFacade> OnEntityDestroyed;

        [Inject]
        public void Construct(AsteroidDeathHandler asteroidDeathHandler,
            AsteroidEntity asteroidEntity)
        {
            _asteroidDeathHandler = asteroidDeathHandler;
            AsteroidEntity = asteroidEntity;
        }

        public void ReceiveDamage(HitData hitData)
        {
            AsteroidEntity.ReceiveDamage(hitData);
            Die(hitData.AttackApplier);
        }
        public void OnDespawned()
        {
            _pool = null;
            
            OnEntityDestroyed?.Invoke(this);  
        }

        public void OnSpawned(float lifeTime,  Vector3 direction, IMemoryPool pool)
        {
            _pool = pool;
            
            OnEntitySpawned?.Invoke(this);

            _lifeTime = lifeTime;
            
            AsteroidEntity.RandomizeSize();
            AsteroidEntity.SetDirection(direction);
            
            this.CallWithDelay(Dispose, _lifeTime);
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
        }

        public void ApplyAttack(HitData hitData, IDamageReceiver damageReceiver)
        {
            damageReceiver.ReceiveDamage(hitData);
        }
        
        private void Die(IAttackApplier attackApplier)
        {
            _asteroidDeathHandler.Die(attackApplier);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.TryGetComponent(out IDamageReceiver hurtable))
            {
                ApplyAttack(new HitData
                {
                    Damage = AsteroidEntity.StatsData.GetStat(Stats.Damage) * AsteroidEntity.Size
                }, hurtable);
                
                Die(this);
            }
        }

        public class Factory : PlaceholderFactory<float,  Vector3, AsteroidFacade> { }

    }
}