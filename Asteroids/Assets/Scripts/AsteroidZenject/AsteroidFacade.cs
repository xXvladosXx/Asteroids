using System;
using Entities;
using UnityEngine;
using Zenject;

namespace AsteroidZenject
{
    public class AsteroidFacade : MonoBehaviour, IPoolable<Vector3, IMemoryPool>, IDisposable
    {
        public AsteroidEntity AsteroidEntity { get; private set; }

        private IMemoryPool _pool;
        private AsteroidRegistry _asteroidRegistry;
        private AsteroidDeathHandler _asteroidDeathHandler;

        [Inject]
        public void Construct(AsteroidRegistry asteroidRegistry,
            AsteroidDeathHandler asteroidDeathHandler,
            AsteroidEntity asteroidEntity)
        {
            _asteroidRegistry = asteroidRegistry;
            _asteroidDeathHandler = asteroidDeathHandler;
            AsteroidEntity = asteroidEntity;
        }
        
        public void OnDespawned()
        {
            _asteroidRegistry.RemoveEnemy(this);
            _pool = null;
        }

        public void OnSpawned(Vector3 direction, IMemoryPool pool)
        {
            _pool = pool;
            _asteroidRegistry.AddEnemy(this);
            
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
        
        public class Factory : PlaceholderFactory<Vector3, AsteroidFacade> { }
    }
}