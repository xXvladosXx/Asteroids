using System;
using AsteroidZenject;
using EnemiesZenject;
using Entities;
using Spawners.Core;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace EnemyShipZenject
{
    public class EnemyShipSpawner : EntitySpawner, IInitializable, IDisposable, ITickable
    {
        private readonly EnemyShipRegistry _enemyShipRegistry;
        private readonly PlayerEntity _playerEntity;

        private EnemyShipFacade.Factory _enemyShipFactory;
        private SignalBus _signalBus;
        private Settings _settings;

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
        }

        public void Dispose()
        {
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Transform[] PointsToSpawn { get; private set; }
        }
    }
}