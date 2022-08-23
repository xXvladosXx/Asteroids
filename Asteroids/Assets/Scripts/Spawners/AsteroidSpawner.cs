using System;
using AsteroidZenject;
using Data.Score;
using Entities;
using ObjectPoolers;
using Spawners.Core;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class AsteroidSpawner : EntitySpawner, IInitializable, IDisposable, ITickable
    {
        private AsteroidFacade.Factory _asteroidFactory;
        private SignalBus _signalBus;
        private Settings _settings;
        public event Action<float> OnScoreAdded;

        public AsteroidSpawner(AsteroidFacade.Factory factory,
            SignalBus signalBus,
            Settings settings)
        {
            _asteroidFactory = factory;
            _signalBus = signalBus;
            _settings = settings;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<AsteroidKilledSignal>(OnAsteroidDestroyed);
            
            Spawn();
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Spawn();
            }
        }
        
        public void Dispose()
        {
        }
        public override void Spawn()
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * _settings.SpawnDistance;
            Vector3 spawnPoint = Vector3.zero + spawnDirection;

            float variance = Random.Range(-_settings.DirectionVariance, _settings.DirectionVariance); 
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            var asteroid = _asteroidFactory.Create(rotation * -spawnDirection);
            asteroid.transform.position = spawnPoint;
            asteroid.transform.rotation = rotation;
            
            //asteroid.RandomizeSize();
            //asteroid.SetDirection();
            
            for (int i = 0; i < SpawnAmount; i++)
            {
               /* 
                asteroid.OnAsteroidDestroyed += OnAsteroidDestroyed;
                asteroid.OnAsteroidReleased += OnAsteroidReleased; */
            } 
        }

        private void OnAsteroidReleased(AsteroidEntity asteroidEntity)
        {
            ReleaseAsteroid(asteroidEntity);
        }

        private void OnAsteroidDestroyed()
        {
            
        }
        
        private void OnAsteroidDestroyed(AsteroidEntity asteroidEntity)
        {
            if (asteroidEntity.Size > asteroidEntity.AsteroidData.SizeToSplit)
            {
                // var firstSplit = _asteroidFactory.Create();
                // asteroidEntity.CreateSplit(firstSplit);
                // firstSplit.OnAsteroidDestroyed += OnAsteroidDestroyed;
                // firstSplit.OnAsteroidReleased += OnAsteroidReleased;
                //
                // var secondSplit = _asteroidFactory.Create();
                // asteroidEntity.CreateSplit(secondSplit);
                // secondSplit.OnAsteroidDestroyed += OnAsteroidDestroyed;
                // secondSplit.OnAsteroidReleased += OnAsteroidReleased;
            }
            
            OnScoreAdded?.Invoke(asteroidEntity.Size);
            ReleaseAsteroid(asteroidEntity);
        }
        
        private void ReleaseAsteroid(AsteroidEntity asteroidEntity)
        {
            asteroidEntity.OnAsteroidReleased -= OnAsteroidReleased;
            asteroidEntity.OnAsteroidDestroyed -= OnAsteroidDestroyed;

            //AsteroidPool.Instance.ReleasePrefab(asteroidEntity);
            
            asteroidEntity.RandomizeSize();
        }
        
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float SpawnDistance { get; private set; } = 10;
            [field: SerializeField] public float DirectionVariance { get; private set; } = 10;
        }
    }
}