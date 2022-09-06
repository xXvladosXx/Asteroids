using System;
using Entities;
using Entities.Core;
using UnityEngine;
using Zenject;

namespace AsteroidsZenject.EnemyShipZenject
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
        public void Construct(EnemyShip enemyShip,
            PlayerEntity playerEntity)
        {
            PlayerEntity = playerEntity;

            EnemyShip = enemyShip;
        }

        public void Init()
        {
            EnemyShip.Target = PlayerEntity.transform;
            EnemyShip.Init();
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