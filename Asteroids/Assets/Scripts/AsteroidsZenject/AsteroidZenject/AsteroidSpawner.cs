using System;
using Spawners.Core;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AsteroidsZenject.AsteroidZenject
{
    public class AsteroidSpawner : EntitySpawner, IInitializable, ITickable
    {
        private readonly AsteroidRegistry _asteroidRegistry;
        private readonly AsteroidFacade.Factory _asteroidFactory;
        private readonly SignalBus _signalBus;
        private readonly Settings _settings;

        private float _desiredNumEnemies;
        private int _enemyCount;
        private float _lastSpawnTime;

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
            _signalBus.Subscribe<EntityKilledSignal>(OnAsteroidDestroyed);
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Spawn();
            }

            _desiredNumEnemies += _settings.NumEnemiesIncreaseRate * Time.deltaTime;

            if (_enemyCount < (int) _desiredNumEnemies
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
            asteroid.OnEntityDestroyed += OnAsteroidDestroyed;

            asteroid.transform.position = spawnPoint;
            asteroid.transform.rotation = rotation;

            _lastSpawnTime = Time.realtimeSinceStartup;
        }

        private void OnAsteroidDestroyed(AsteroidFacade asteroidFacade)
        {
            if (asteroidFacade.AsteroidEntity.Size > asteroidFacade.AsteroidEntity.AsteroidData.SizeToSplit)
            {
                Vector3 spawnDirection = Random.insideUnitCircle.normalized * _settings.SpawnDistance;
                float variance = Random.Range(-_settings.DirectionVariance, _settings.DirectionVariance);
                Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                var firstSplit = _asteroidFactory.Create(_settings.AsteroidLifetime, rotation * -spawnDirection);
                asteroidFacade.AsteroidEntity.CreateSplit(firstSplit.AsteroidEntity);

                _asteroidRegistry.AddEnemy(firstSplit);
                firstSplit.OnEntityDestroyed += OnAsteroidDestroyed;

                var secondSplit = _asteroidFactory.Create(_settings.AsteroidLifetime, rotation * spawnDirection);
                asteroidFacade.AsteroidEntity.CreateSplit(secondSplit.AsteroidEntity);

                _asteroidRegistry.AddEnemy(secondSplit);
                secondSplit.OnEntityDestroyed += OnAsteroidDestroyed;
            }

            asteroidFacade.OnEntityDestroyed -= OnAsteroidDestroyed;

            _asteroidRegistry.RemoveEnemy(asteroidFacade);
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