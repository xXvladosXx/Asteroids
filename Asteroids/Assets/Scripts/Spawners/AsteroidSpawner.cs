using System;
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
        public override void Spawn()
        {
            for (int i = 0; i < SpawnAmount; i++)
            {
                Vector3 spawnDirection = Random.insideUnitCircle.normalized * SpawnDistance;
                Vector3 spawnPoint = transform.position + spawnDirection;

                float variance = Random.Range(-DirectionVariance, DirectionVariance); 
                Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
                
                var asteroid = Instantiate(EntityToSpawn, spawnPoint, rotation) as AsteroidEntity;
                asteroid.Size = Random.Range(asteroid.AsteroidData.MinSize, asteroid.AsteroidData.MaxSize);
                asteroid.SetDirection(rotation * -spawnDirection);
            } 
        }
    }
}