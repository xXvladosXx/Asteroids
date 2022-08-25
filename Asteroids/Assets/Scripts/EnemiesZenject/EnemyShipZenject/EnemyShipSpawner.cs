using System;
using AsteroidZenject;
using EnemiesZenject;
using EnemiesZenject.AsteroidZenject;
using Spawners.Core;
using UnityEngine;
using Zenject;

namespace EnemyShipZenject
{
    public class EnemyShipSpawner : EntitySpawner, IInitializable, IDisposable, ITickable
    {
        private EnemyFactory _customEnemyFactory;
        private SignalBus _signalBus;
        private Settings _settings;
        
        public EnemyShipSpawner(EntityRegistry entityRegistry,
            EnemyFactory customEnemyFactory) : base(entityRegistry)
        {
            _customEnemyFactory = customEnemyFactory;
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
            var enemyShip = _customEnemyFactory.Create();
            
            enemyShip.OnEntitySpawned += EntityRegistry.AddEnemy;
            enemyShip.OnEntityDestroyed += EntityRegistry.RemoveEnemy;
        }
        
        public void Dispose()
        {
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float SpawnDistance { get; private set; } = 10;
            [field: SerializeField] public float DirectionVariance { get; private set; } = 10;
        }
    }
}