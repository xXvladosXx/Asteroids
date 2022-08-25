using System;
using AsteroidZenject;
using Data.Score;
using EnemiesZenject;
using EnemiesZenject.AsteroidZenject;
using Entities;
using ObjectPoolers;
using Spawners.Core;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class AsteroidSpawner : EntitySpawner, IInitializable, ITickable
    {
        private AsteroidFacade.Factory _asteroidFactory;
        private SignalBus _signalBus;
        private Settings _settings;

        public AsteroidSpawner(AsteroidFacade.Factory factory,
            SignalBus signalBus,
            Settings settings,
            EntityRegistry entityRegistry) : base(entityRegistry)
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
        
        public override void Spawn()
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * _settings.SpawnDistance;
            Vector3 spawnPoint = Vector3.zero + spawnDirection;

            float variance = Random.Range(-_settings.DirectionVariance, _settings.DirectionVariance); 
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            var asteroid = _asteroidFactory.Create(rotation * -spawnDirection);
            
            asteroid.OnEntitySpawned += EntityRegistry.AddEnemy;
            asteroid.OnEntityDestroyed += EntityRegistry.RemoveEnemy;
            
            asteroid.transform.position = spawnPoint;
            asteroid.transform.rotation = rotation;
            
        }

        private void OnAsteroidDestroyed() { }
        
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float SpawnDistance { get; private set; } = 10;
            [field: SerializeField] public float DirectionVariance { get; private set; } = 10;
        }
    }
}