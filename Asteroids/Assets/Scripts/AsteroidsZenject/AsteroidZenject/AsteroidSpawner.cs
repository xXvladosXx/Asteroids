using System;
using AsteroidZenject;
using Data.Score;
using EnemiesZenject;
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
        private readonly AsteroidRegistry _asteroidRegistry;
        private readonly AsteroidFacade.Factory _asteroidFactory;
        private readonly SignalBus _signalBus;
        private readonly Settings _settings;

        float _desiredNumEnemies;
        int _enemyCount;
        float _lastSpawnTime;
        
        public AsteroidSpawner(AsteroidFacade.Factory factory,
            SignalBus signalBus,
            Settings settings,
            AsteroidRegistry asteroidRegistry)
        {
            _asteroidFactory = factory;
            _signalBus = signalBus;
            _settings = settings;
            _asteroidRegistry = asteroidRegistry;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<AsteroidKilledSignal>(OnAsteroidDestroyed);
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Spawn();
            }
            
            _desiredNumEnemies += _settings.NumEnemiesIncreaseRate * Time.deltaTime;

            if (_enemyCount < (int)_desiredNumEnemies
                && Time.realtimeSinceStartup - _lastSpawnTime > _settings.MinDelayBetweenSpawns)
            {
                Spawn();
                _enemyCount++;
            }
        }
        
        public override void Spawn()
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * _settings.SpawnDistance;
            Vector3 spawnPoint = Vector3.zero + spawnDirection;

            float variance = Random.Range(-_settings.DirectionVariance, _settings.DirectionVariance); 
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            var asteroid = _asteroidFactory.Create(_settings.AsteroidLifetime, rotation * -spawnDirection);
            
            _asteroidRegistry.AddEnemy(asteroid);
            asteroid.OnEntityDestroyed += _asteroidRegistry.RemoveEnemy;
            
            asteroid.transform.position = spawnPoint;
            asteroid.transform.rotation = rotation;
            
            _lastSpawnTime = Time.realtimeSinceStartup;
        }

        private void OnAsteroidDestroyed()
        {
            _enemyCount--;
        }
        
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float SpawnDistance { get; private set; } = 10;
            [field: SerializeField] public float DirectionVariance { get; private set; } = 10;
            [field: SerializeField] public float NumEnemiesIncreaseRate { get; private set; }
            [field: SerializeField] public float NumEnemiesStartAmount { get; private set; }
            [field: SerializeField] public float MinDelayBetweenSpawns { get; private set; } = 0.5f;
            [field: SerializeField] public float AsteroidLifetime { get; private set; } = 2f;
            
        }
    }
}