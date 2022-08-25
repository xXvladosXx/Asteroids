using System;
using Combat.Core;
using EnemiesZenject;
using Entities;
using Entities.Core;
using UnityEngine;
using Zenject;

namespace EnemyShipZenject
{
    public class EnemyShipFacade : EntityFacade, IPoolable<IMemoryPool>, //how to send a collider? 
        IDisposable, IDamageReceiver, IEnemy
    {
        private EnemyShip EnemyShip { get; set; }
        public PlayerEntity PlayerEntity { get; set; }
        
        private IMemoryPool _pool;

        public event Action<EntityFacade> OnEntitySpawned;
        public event Action<EntityFacade> OnEntityDestroyed;

        public void Construct()
        {
            EnemyShip.Construct();
        }
        
        [Inject]
        public void Construct(EnemyShip enemyShip,
            PlayerEntity playerEntity)
        {
            EnemyShip = enemyShip;
            PlayerEntity = playerEntity;
        }
        
        public void OnDespawned()
        {
            _pool = null;
            
            OnEntityDestroyed?.Invoke(this);  
        }

        public void ReceiveDamage(HitData hitData)
        {
            Debug.Log("Damaged ship");
            EnemyShip.ReceiveDamage(hitData);
        }
        
        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            OnEntitySpawned?.Invoke(this);
            
            Debug.Log("Spawned enemy");
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }
        
        public class Factory : PlaceholderFactory<EnemyShipFacade> { }
        
    }
}