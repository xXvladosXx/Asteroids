using System;
using AsteroidsZenject.EnemyShipZenject;
using AsteroidsZenject.ExplosionZenject;
using Combat.Core;
using EnemyShipZenject;
using UnityEngine;
using Zenject;

namespace EnemiesZenject.EnemyShipZenject
{
    public class EnemyShipDeathHandler : IInitializable, IDisposable
    {
        private readonly EnemyShipFacade _enemyShipFacade;
        private readonly SignalBus _signalBus;
        private readonly EnemyExplosion.Factory _explosionFactory;

        public EnemyShipDeathHandler(EnemyShipFacade enemyShipFacade,
            SignalBus signalBus,
            EnemyExplosion.Factory explosionFactory)
        {
            _enemyShipFacade = enemyShipFacade;
            _signalBus = signalBus;
            _explosionFactory = explosionFactory;
        }
        
        public void Initialize()
        {
            _enemyShipFacade.EnemyShip.OnDied += Die;
        }

        private void Die(IAttackApplier attackApplier)
        {
            var explosion = _explosionFactory.Create();
            explosion.transform.position = _enemyShipFacade.EnemyShip.transform.position;
            
            _signalBus.Fire(new EntityKilledSignal(_enemyShipFacade.EnemyShip, attackApplier));
            
            _enemyShipFacade.Dispose();
        }

        public void Dispose()
        {
            _enemyShipFacade.EnemyShip.OnDied -= Die;
        }
    }
}