using System;
using Data.Score;
using Entities;
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
                
                asteroid.Size = Random.Range(asteroid.AsteroidData.MinSize, asteroid.AsteroidData.MaxSize);
                asteroid.SetDirection(rotation * -spawnDirection);

                asteroid.OnAsteroidDestroyed += OnAsteroidDestroyed;
            } 
        }

        private void OnAsteroidDestroyed(AsteroidEntity asteroidEntity)
        {
            asteroidEntity.OnAsteroidDestroyed -= OnAsteroidDestroyed;
            
            AsteroidPool.Instance.ReleasePrefab(asteroidEntity);

            OnScoreAdded?.Invoke(asteroidEntity.Size);
        }
    }
}