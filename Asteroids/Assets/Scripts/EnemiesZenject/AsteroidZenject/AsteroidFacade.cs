using System;
using Combat.Core;
using EnemiesZenject;
using Entities;
using Entities.Core;
using StatsSystem.Core;
using UnityEngine;
using Zenject;

namespace AsteroidZenject
{
    public class AsteroidFacade : EntityFacade, IPoolable<Vector3, IMemoryPool>,
        IDisposable, IDamageReceiver, IEnemy
    {
        private AsteroidEntity AsteroidEntity { get; set; }

        private IMemoryPool _pool;
        private AsteroidDeathHandler _asteroidDeathHandler;
       
        public event Action<EntityFacade> OnEntitySpawned;
        public event Action<EntityFacade> OnEntityDestroyed;
        
        [Inject]
        public void Construct(AsteroidDeathHandler asteroidDeathHandler,
            AsteroidEntity asteroidEntity)
        {
            _asteroidDeathHandler = asteroidDeathHandler;
            AsteroidEntity = asteroidEntity;
        }
        
        public PlayerEntity PlayerEntity { get; set; }

        public void ReceiveDamage(HitData hitData)
        {
            AsteroidEntity.ReceiveDamage(hitData);
            
            Die();
        }
        public void OnDespawned()
        {
            _pool = null;
            OnEntityDestroyed?.Invoke(this);  
            Debug.Log("destroy asteroid");
        }

        public void OnSpawned(Vector3 direction, IMemoryPool pool)
        {
            _pool = pool;
            OnEntitySpawned?.Invoke(this);
            Debug.Log("spawned asteroid");
            
            AsteroidEntity.RandomizeSize();
            AsteroidEntity.SetDirection(direction);
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public void Die()
        {
            _asteroidDeathHandler.Die();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.TryGetComponent(out IDamageReceiver hurtable))
            {
                AsteroidEntity.ApplyAttack(hurtable);
                
                Die();
            }
        }

        public class Factory : PlaceholderFactory<Vector3, AsteroidFacade> { }
    }
}