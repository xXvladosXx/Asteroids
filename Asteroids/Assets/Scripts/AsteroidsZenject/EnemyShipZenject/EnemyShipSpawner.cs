using System;
using Data.EnemyShip;
using Entities;
using Spawners.Core;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AsteroidsZenject.EnemyShipZenject
{
    public class EnemyShipSpawner : EntitySpawner, IInitializable, IDisposable, ITickable
    {
        private readonly EnemyShipRegistry _enemyShipRegistry;
        private readonly PlayerEntity _playerEntity;

        private EnemyShipFacade.Factory _enemyShipFactory;
        private SignalBus _signalBus;
        private EnemyShipSpawnerData _enemyShipSpawnerData;

        private float _desiredNumEnemies;
        private int _enemyCount;
        private float _lastSpawnTime;
        
        public EnemyShipSpawner(EnemyShipRegistry enemyShipRegistry,
            EnemyShipFacade.Factory enemyShipFactory, 
            PlayerEntity playerEntity,
            EnemyShipSpawnerData enemyShipSpawnerData) 
        {
            _enemyShipFactory = enemyShipFactory;
            _enemyShipRegistry = enemyShipRegistry;
            _playerEntity = playerEntity;
            _enemyShipSpawnerData = enemyShipSpawnerData;
        }
        
        public void Initialize()
        {
        }
        
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Spawn();
            }
            
            _desiredNumEnemies += _enemyShipSpawnerData.NumEnemiesIncreaseRate * Time.deltaTime;

            if (_enemyCount < (int) _desiredNumEnemies
                && Time.realtimeSinceStartup - _lastSpawnTime > _enemyShipSpawnerData.MinDelayBetweenSpawns)
            {
                Spawn();
                _enemyCount++;
            }
        }
        
        public override void Spawn()
        {
            int pointToSpawn = Random.Range(0, _enemyShipSpawnerData.PointsToSpawn.Length);

            Vector3 position = _enemyShipSpawnerData.PointsToSpawn[pointToSpawn];
            var enemy = _enemyShipFactory.Create(position);
            enemy.Init();

            _enemyShipRegistry.AddEnemy(enemy);
            enemy.OnEntityDestroyed += _enemyShipRegistry.RemoveEnemy;
            
            _lastSpawnTime = Time.realtimeSinceStartup;
        }

        public void Dispose()
        {
        }
    }
}