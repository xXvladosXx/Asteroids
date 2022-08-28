using System;
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

        public EnemyShipDeathHandler(EnemyShipFacade enemyShipFacade,
            SignalBus signalBus)
        {
            _enemyShipFacade = enemyShipFacade;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _enemyShipFacade.EnemyShip.OnDied += Die;
        }

        private void Die(IAttackApplier attackApplier)
        {
            _signalBus.Fire(new EnemyShipKilledSignal(_enemyShipFacade.EnemyShip, attackApplier));
            
            _enemyShipFacade.Dispose();
        }


        public void Dispose()
        {
            _enemyShipFacade.EnemyShip.OnDied -= Die;
        }
    }
}