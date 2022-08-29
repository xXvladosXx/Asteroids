using System;
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
        private Settings _settings;

        private float _desiredNumEnemies;
        private int _enemyCount;
        private float _lastSpawnTime;
        
        public EnemyShipSpawner(EnemyShipRegistry enemyShipRegistry,
            EnemyShipFacade.Factory enemyShipFactory, 
            PlayerEntity playerEntity,
            Settings settings) 
        {
            _enemyShipFactory = enemyShipFactory;
            _enemyShipRegistry = enemyShipRegistry;
            _playerEntity = playerEntity;
            _settings = settings;
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
            int pointToSpawn = Random.Range(0, _settings.PointsToSpawn.Length);

            Vector3 position = Vector3.zero;
            
            if (_settings.PointsToSpawn[pointToSpawn] != null)
            {
                position = _settings.PointsToSpawn[pointToSpawn].position;
            }
            
            var enemy = _enemyShipFactory.Create(position);
            enemy.Construct(_playerEntity);

            _enemyShipRegistry.AddEnemy(enemy);
            enemy.OnEntityDestroyed += _enemyShipRegistry.RemoveEnemy;
            
            _lastSpawnTime = Time.realtimeSinceStartup;
        }

        public void Dispose()
        {
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Transform[] PointsToSpawn { get; private set; }
            [field: SerializeField] public float NumEnemiesIncreaseRate { get; private set; }
            [field: SerializeField] public float NumEnemiesStartAmount { get; private set; }
            [field: SerializeField] public float MinDelayBetweenSpawns { get; private set; } = 5f;
        }
    }
}