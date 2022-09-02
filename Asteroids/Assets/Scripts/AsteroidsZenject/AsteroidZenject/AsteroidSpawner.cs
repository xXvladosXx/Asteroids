using System;
using System.Collections.Generic;
using Data.Asteroid;
using Data.Difficulties;
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
        private readonly AsteroidSpawnerData _asteroidSpawnerData;

        private float _desiredNumEnemies;
        private int _enemyCount;
        private float _lastSpawnTime;

        public AsteroidSpawner(AsteroidFacade.Factory factory,
            SignalBus signalBus,
            AsteroidRegistry asteroidRegistry,
            AsteroidSpawnerData asteroidSpawnerData)
        {
            _asteroidFactory = factory;
            _signalBus = signalBus;
            _asteroidRegistry = asteroidRegistry;
            _asteroidSpawnerData = asteroidSpawnerData;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EntityKilledSignal>(OnAsteroidDestroyed);

            Debug.Log(_asteroidSpawnerData.name);
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Spawn();
            }

            _desiredNumEnemies += _asteroidSpawnerData.NumEnemiesIncreaseRate * Time.deltaTime;

            if (_enemyCount < (int) _desiredNumEnemies
                && Time.realtimeSinceStartup - _lastSpawnTime > _asteroidSpawnerData.MinDelayBetweenSpawns)
            {
                Spawn();
                _enemyCount++;
            }
        }

        public override void Spawn()
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * _asteroidSpawnerData.SpawnDistance;
            Vector3 spawnPoint = Vector3.zero + spawnDirection;

            float variance = Random.Range(-_asteroidSpawnerData.DirectionVariance,
                _asteroidSpawnerData.DirectionVariance);

            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            var asteroid = _asteroidFactory.Create(_asteroidSpawnerData.AsteroidLifetime,
                rotation * -spawnDirection);

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
                Vector3 spawnDirection = Random.insideUnitCircle.normalized * _asteroidSpawnerData.SpawnDistance;

                float variance = Random.Range(-_asteroidSpawnerData.DirectionVariance,
                    _asteroidSpawnerData.DirectionVariance);

                Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                var firstSplit = _asteroidFactory.Create(_asteroidSpawnerData.AsteroidLifetime,
                    rotation * -spawnDirection);
                asteroidFacade.AsteroidEntity.CreateSplit(firstSplit.AsteroidEntity);

                _asteroidRegistry.AddEnemy(firstSplit);
                firstSplit.OnEntityDestroyed += OnAsteroidDestroyed;

                var secondSplit = _asteroidFactory.Create(_asteroidSpawnerData.AsteroidLifetime,
                    rotation * spawnDirection);
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
    }
}