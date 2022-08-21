using System;
using Data.Score;
using Entities;
using ObjectPoolers;
using Spawners.Core;
using UnityEngine;
using Zenject.Asteroids;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class AsteroidSpawner : EntitySpawner
    {
        [field: SerializeField] public float SpawnDistance { get; private set; } = 10;
        [field: SerializeField] public float DirectionVariance { get; private set; } = 10;

        public event Action<float> OnScoreAdded;
        public override void Spawn()
        {
            for (int i = 0; i < SpawnAmount; i++)
            {
                Vector3 spawnDirection = Random.insideUnitCircle.normalized * SpawnDistance;
                Vector3 spawnPoint = transform.position + spawnDirection;

                float variance = Random.Range(-DirectionVariance, DirectionVariance); 
                Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                var asteroid = AsteroidPool.Instance.GetPrefab();
                asteroid.transform.position = spawnPoint;
                asteroid.transform.rotation = rotation;
                
                asteroid.RandomizeSize();
                asteroid.SetDirection(rotation * -spawnDirection);

                asteroid.OnAsteroidDestroyed += OnAsteroidDestroyed;
                asteroid.OnAsteroidReleased += OnAsteroidReleased;
            } 
        }

        private void OnAsteroidReleased(AsteroidEntity asteroidEntity)
        {
            ReleaseAsteroid(asteroidEntity);
        }

        private void OnAsteroidDestroyed(AsteroidEntity asteroidEntity)
        {
            if (asteroidEntity.Size > asteroidEntity.AsteroidData.SizeToSplit)
            {
                var firstSplit = AsteroidPool.Instance.GetPrefab();
                asteroidEntity.CreateSplit(firstSplit);
                firstSplit.OnAsteroidDestroyed += OnAsteroidDestroyed;
                firstSplit.OnAsteroidReleased += OnAsteroidReleased;
                
                var secondSplit = AsteroidPool.Instance.GetPrefab();
                asteroidEntity.CreateSplit(secondSplit);
                secondSplit.OnAsteroidDestroyed += OnAsteroidDestroyed;
                secondSplit.OnAsteroidReleased += OnAsteroidReleased;
            }
            
            OnScoreAdded?.Invoke(asteroidEntity.Size);
            ReleaseAsteroid(asteroidEntity);
        }
        
        private void ReleaseAsteroid(AsteroidEntity asteroidEntity)
        {
            asteroidEntity.OnAsteroidReleased -= OnAsteroidReleased;
            asteroidEntity.OnAsteroidDestroyed -= OnAsteroidDestroyed;

            AsteroidPool.Instance.ReleasePrefab(asteroidEntity);
            
            asteroidEntity.RandomizeSize();
        }
    }
}