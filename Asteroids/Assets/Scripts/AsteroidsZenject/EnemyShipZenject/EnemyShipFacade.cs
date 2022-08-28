using System;
using Combat.Core;
using EnemiesZenject;
using Entities;
using Entities.Core;
using UnityEngine;
using Zenject;

namespace EnemyShipZenject
{
    public class EnemyShipFacade : MonoBehaviour, IPoolable<Vector3, IMemoryPool>,
        IDisposable, IEnemy
    {
        [field: SerializeField] public EnemyShip EnemyShip { get; set; }
        public PlayerEntity PlayerEntity { get; set; }

        private IMemoryPool _pool;
        public event Action<EnemyShipFacade> OnEntitySpawned;
        public event Action<EnemyShipFacade> OnEntityDestroyed;

        [Inject]
        public void Construct(EnemyShip enemyShip)
        {
            EnemyShip = enemyShip;
        }

        public void Construct(PlayerEntity playerEntity)
        {
            PlayerEntity = playerEntity;

            EnemyShip.Target = PlayerEntity.transform;

            EnemyShip.Construct();
        }

        public void OnDespawned()
        {
            _pool = null;

            OnEntityDestroyed?.Invoke(this);
        }

        public void OnSpawned(Vector3 spawnPoint, IMemoryPool pool)
        {
            _pool = pool;

            transform.position = spawnPoint;

            OnEntitySpawned?.Invoke(this);
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<Vector3, EnemyShipFacade>
        {
        }
    }
}